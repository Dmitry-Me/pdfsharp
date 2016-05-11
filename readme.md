PDFsharp with SharpZipLib replaced with DotNetZip
=========

This is a fork of [PDFsharp](http://pdfsharp.codeplex.com) version 1.32 where SharpZipLib code (GPL with GNU Classpath exception) is replaced with [DotNetZip](http://dotnetzip.codeplex.com) (Ms-PL) version 1.9.1.8.

What??? Why???
-------------
Two reasons:
 
 1. Seeing "GPL" abbreviation makes proprietary code developers suspicious, no matter the GNU Classpath exception. Analysts and lawers are then involved, numerous emails and meetings start.
 2. GNU Classpath exception allows to "link" the licensed library with other modules (and distribute the result under any license) but PDFsharp includes SharpZipLib source as a subtree which doesn't quite look as "linking" which in turn looks like SharpZipLib license violation and the latter is not something properitary code developers want to test in court.

Why not move SharpZipLib into a separate assembly then?
-------------
Two reasons:

 1. "GPL" abbreviation would persist.
 2. A yet another assembly in the dependencies list doesn't make anyone happy.

What about System.IO.Compression.DeflateStream?
------------

Yes, there is NET_ZIP compiler symbol in the original code which switches between SharpZipLib and System.IO.Compression.DeflateStream but DeflateStream implements RFC 1951 and PDF needs RFC 1950 compression. See http://stackoverflow.com/a/18450408

So what's here?
------------

PDFsharp 1.32 is cleaned of SharpZipLib code completely. No more "GPL" abbreviation. DotNetZip ZlibStream is used instead. Code using DeflateStream is removed because it just doesn't work.

License
-------

MIT License (please note that Ms-PL code (DotNetZip) is contained in a subtree which shouldn't be a problem):

Copyright (c) 2005-2016 empira Software GmbH and other contributors

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
