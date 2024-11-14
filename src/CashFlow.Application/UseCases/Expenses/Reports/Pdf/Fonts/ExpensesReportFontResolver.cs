using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
public class ExpensesReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream =  ReadFontFile(faceName);

        stream ??= ReadFontFile(FontHelper.DEFAULT_FONT); //se o valor for nulo

        var length = (int)stream!.Length;

        var data = new byte[length];

        stream.Read(buffer: data, offset: 0, count: length);
        
        return data;
    } //leitura dos arquivos de fonte

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

    private Stream? ReadFontFile(string faceName)
    { 
        var assembly = Assembly.GetExecutingAssembly(); // esta função consegue ler como stream o arquivo de fonte

        return assembly.GetManifestResourceStream($"CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts.{faceName}.ttf"); 
    }
}
