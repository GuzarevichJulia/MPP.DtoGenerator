using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using DTO_Generator.ClassDescription;
using DTO_Generator.TypesTableDescription;

namespace DTO_Generator
{
    public class CodeGenerator : ICodeGenerator
    {
        private TypesTable types = new TypesTable();

        public void GenerateCode(object data)
        {
                Task task = data as Task;
                string classNamespace = task.ClassNamespace;
            {
                SyntaxTree result = GenerateClassCode(task);
                if (result != null)
                {
                    task.GeneratedClass.SyntaxTree = result;
                }
                task.Event.Set();
            }
        }

        private SyntaxTree GenerateClassCode(Task task)
        {
            SyntaxList<MemberDeclarationSyntax> memberList = new SyntaxList<MemberDeclarationSyntax>();
            foreach (Property property in task.Class.Properties)
            {
                MemberDeclarationSyntax propertyCode = GeneratePropertyCode(property);
                if (propertyCode != null)
                {
                    memberList = memberList.Add(propertyCode);
                }
                else
                {
                    return null;
                }
            }
            CompilationUnitSyntax compilationUnit = CompilationUnit()
            .WithUsings(
                SingletonList<UsingDirectiveSyntax>(
                    UsingDirective(
                        IdentifierName("System"))))
            .WithMembers(
                SingletonList<MemberDeclarationSyntax>(
                    NamespaceDeclaration(
                        IdentifierName(task.ClassNamespace))
                    .WithMembers(
                        SingletonList<MemberDeclarationSyntax>(
                            ClassDeclaration(task.Class.ClassName)
                            .WithModifiers(
                                TokenList(
                                    Token(SyntaxKind.PublicKeyword)))
                            .WithMembers(memberList)))))
            .NormalizeWhitespace();

            return compilationUnit.SyntaxTree;
            

        }

        private MemberDeclarationSyntax GeneratePropertyCode(Property property)
        {
            string netType = types.GetNetType(property.Type, property.Format);
            MemberDeclarationSyntax propertyCode;
            if (netType != "")
            {
                propertyCode = PropertyDeclaration(
                                                    IdentifierName(netType),
                                                    Identifier(property.Name))
                                                .WithModifiers(
                                                    TokenList(
                                                        Token(SyntaxKind.PublicKeyword)))
                                                .WithAccessorList(
                                                    AccessorList(
                                                        List<AccessorDeclarationSyntax>(
                                                            new AccessorDeclarationSyntax[]{
                                                    AccessorDeclaration(
                                                        SyntaxKind.GetAccessorDeclaration)
                                                    .WithSemicolonToken(
                                                        Token(SyntaxKind.SemicolonToken)),
                                                    AccessorDeclaration(
                                                        SyntaxKind.SetAccessorDeclaration)
                                                    .WithSemicolonToken(
                                                        Token(SyntaxKind.SemicolonToken))})));
                return propertyCode;
            }
            else
            {
                LogPrinter.Output("Type \"" + property.Type + "\" with format \"" + property.Format + "\" not found");
                return null;
            }
        }
    }
}
