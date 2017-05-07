// Author:
// Evgeny Zborovsky
// https://github.com/yuv4ik
//
// Copyright (c) 2017 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttpGzipPlayground
{
	public class JsonContent : HttpContent
	{
		private JsonSerializer serializer { get; }
		private object value { get; }

		public JsonContent(object value)
		{
			this.serializer = new JsonSerializer();
			this.value = value;
			Headers.ContentType = new MediaTypeHeaderValue("application/json");
			Headers.ContentEncoding.Add("gzip");
		}

		protected override bool TryComputeLength(out long length)
		{
			length = -1;
			return false;
		}

		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			return Task.Factory.StartNew(() =>
			{
				using (var gzip = new GZipStream(stream, CompressionMode.Compress, true))
				using (var writer = new StreamWriter(gzip))
				{
					serializer.Serialize(writer, value);
				}
			});
		}
	}
}
