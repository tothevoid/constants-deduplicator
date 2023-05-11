using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace ConstantsDeduplication.Core
{
    public class ConstantsCollector : CSharpSyntaxWalker
    {
        public event Action<HashSet<string>> OnElementsLoaded;

        private HashSet<string> Constants { get; } = new HashSet<string>();

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            if (node == null)
            {
                return;
            }

            foreach (ClassDeclarationSyntax namespaceClass in node.ChildNodes()
                .Where(subNode => subNode is ClassDeclarationSyntax))
            {
                ParseMetadataRecursively(namespaceClass);
            }

            OnElementsLoaded?.Invoke(Constants);
        }

        public void ParseMetadataRecursively(ClassDeclarationSyntax classDeclaration)
        {
            foreach (var classElement in classDeclaration.ChildNodes())
            {
                if (classElement is FieldDeclarationSyntax fieldDeclaration)
                {
                    var expr = fieldDeclaration.Declaration.Variables.FirstOrDefault()?.Initializer?.Value;
                    if (expr is InvocationExpressionSyntax invocationExpression)
                    {
                        var guidLiteral = invocationExpression?.ArgumentList?.Arguments
                            .FirstOrDefault().GetText().ToString().Replace("\"", string.Empty);
                        Constants.Add(guidLiteral);
                    }
                    else if (expr is LiteralExpressionSyntax literalExpression)
                    {
                        Constants.Add(literalExpression.Token.Value as string);
                    }

                }
                else if (classElement is PropertyDeclarationSyntax propertyDeclaration)
                {
                    var lambdaValue = propertyDeclaration?.ExpressionBody?.Expression?.ChildTokens().FirstOrDefault().Value as string;

                    if (lambdaValue == null)
                    {
                        var r = propertyDeclaration.Initializer?.Value?.GetFirstToken().Value as string;
                        Constants.Add(r);
                    }
                    else
                    {
                        Constants.Add(lambdaValue);
                    }
                }

                if (classElement is ClassDeclarationSyntax subClass)
                {
                    ParseMetadataRecursively(subClass);
                }
            }
        }
    }
}
