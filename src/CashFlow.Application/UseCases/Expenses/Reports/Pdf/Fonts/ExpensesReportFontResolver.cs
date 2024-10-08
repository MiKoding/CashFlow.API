using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
public class ExpensesReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        throw new NotImplementedException();
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        var font = new Font
        {
            Name = "WorkSans",
            Bold = true,
            Italic = false,
        };

        return new FontResolverInfo("WorkSans");
    }
}
