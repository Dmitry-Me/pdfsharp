#region PDFsharp - A .NET library for processing PDF
//
// Authors:
//   Stefan Lange (mailto:Stefan.Lange@pdfsharp.com)
//
// Copyright (c) 2005-2009 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Diagnostics;
using System.Text;
using System.IO;
using PdfSharp.DotNetZip.Ionic.Zlib;

namespace PdfSharp.Pdf.Filters
{
  /// <summary>
  /// Implements the FlateDecode filter by wrapping SharpZipLib.
  /// </summary>
  public class FlateDecode : Filter
  {
    /// <summary>
    /// Encodes the specified data.
    /// </summary>
    public override byte[] Encode(byte[] data)
    {
      MemoryStream ms = new MemoryStream();
      using (var compressor = new ZlibStream(ms, CompressionMode.Compress, true))
      {
        compressor.Write(data, 0, data.Length);
        compressor.Close();
      }
      ms.Capacity = (int)ms.Length;
      return ms.GetBuffer();
    }

    /// <summary>
    /// Decodes the specified data.
    /// </summary>
    public override byte[] Decode(byte[] data, FilterParms parms)
    {
      MemoryStream msInput = new MemoryStream(data);
      MemoryStream msOutput = new MemoryStream();
      using (var decompressor = new ZlibStream(msInput, CompressionMode.Decompress, true))
      {
        int cbRead;
        byte[] abResult = new byte[1024];
        do
        {
          cbRead = decompressor.Read(abResult, 0, abResult.Length);
          if (cbRead > 0)
            msOutput.Write(abResult, 0, cbRead);
        }
        while (cbRead > 0);
        decompressor.Close();
      }
      msOutput.Flush();
      if (msOutput.Length >= 0)
      {
        msOutput.Capacity = (int)msOutput.Length;
        return msOutput.GetBuffer();
      }
      return null;
    }
  }
}
