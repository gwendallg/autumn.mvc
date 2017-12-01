//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from AutumnQuery.g by ANTLR 4.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Autumn.Mvc.Models.Queries {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="AutumnQueryParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6")]
[System.CLSCompliant(false)]
public interface IAutumnQueryVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.selector"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSelector([NotNull] AutumnQueryParser.SelectorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.eval"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEval([NotNull] AutumnQueryParser.EvalContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.or"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOr([NotNull] AutumnQueryParser.OrContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.and"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnd([NotNull] AutumnQueryParser.AndContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.constraint"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstraint([NotNull] AutumnQueryParser.ConstraintContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.group"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroup([NotNull] AutumnQueryParser.GroupContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.comparison"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComparison([NotNull] AutumnQueryParser.ComparisonContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.comparator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComparator([NotNull] AutumnQueryParser.ComparatorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.comp_fiql"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComp_fiql([NotNull] AutumnQueryParser.Comp_fiqlContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.comp_alt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComp_alt([NotNull] AutumnQueryParser.Comp_altContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.reserved"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReserved([NotNull] AutumnQueryParser.ReservedContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.single_quote"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSingle_quote([NotNull] AutumnQueryParser.Single_quoteContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.double_quote"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDouble_quote([NotNull] AutumnQueryParser.Double_quoteContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.arguments"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArguments([NotNull] AutumnQueryParser.ArgumentsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutumnQueryParser.value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitValue([NotNull] AutumnQueryParser.ValueContext context);
}
} // namespace Autumn.Mvc.Models.Queries
