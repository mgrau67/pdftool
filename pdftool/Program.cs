using System;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace pdftool
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Usage();
                return;
            }

            string opt = args[0];
            if (opt == "extract" && args.Length == 5)
            {
                string ifile = args[1];
                string ofile = args[2];
                int.TryParse(args[3], out int from);
                int.TryParse(args[4], out int to);
                var ipdf = PdfReader.Open(ifile, PdfDocumentOpenMode.Import);
                var opdf = new PdfDocument();
                for (int i = from; i <= to; ++i)
                    opdf.AddPage(ipdf.Pages[i]);
                opdf.Save(ofile);
            }
            else
                Usage();
        }

        static void Usage()
        {
            Console.WriteLine("usage: extract <in-pdf> <out-pdf> <from> <to>");
        }
    }
}
