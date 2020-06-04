using System;
using System.IO;
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
            switch (opt)
            {
                case "extract":
                    Extract(args);
                    break;
                case "join":
                    Join(args);
                    break;
                default:
                    Usage();
                    break;
            }
        }

        static void Extract(string[] args)
        {
            if (args.Length == 5)
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

        static void Join(string[] args)
        {
            if (args.Length == 3)
            {
                string ifolder = args[1];
                string ofile = args[2];

                var opdf = new PdfDocument();
                string[] afiles = Directory.GetFiles(ifolder, "*.pdf");
                for (int i = 0; i < afiles.Length; i++)
                {
                    var ipdf = PdfReader.Open(afiles[i], PdfDocumentOpenMode.Import);
                    for (int j = 0; j < ipdf.Pages.Count; ++ j)
                        opdf.AddPage(ipdf.Pages[j]);
                }
                opdf.Save(ofile);
            }
            else
                Usage();
        }

        static void Usage()
        {
            Console.WriteLine(
                "usage:\n" +
                "\textract <in-file> <out-file> <from> <to>\n" +
                "\tjoin <in-folder> <out-file>"
            );
        }
    }
}
