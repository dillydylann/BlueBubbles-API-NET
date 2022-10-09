// This file is a part of the BlueBubbles .NET API library
// Copyright (c) 2022 Dylan Briedis <dylan@dylanbriedis.com>

using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Serialization;

#pragma warning disable SA1600

namespace BlueBubbles.API
{
    internal static class Utils
    {
        private static readonly CamelCaseNamingStrategy CamelCaseNaming = new CamelCaseNamingStrategy();

        public static string StringFormatThrowNulls(string format, params string[] args)
        {
            args = args ?? throw new ArgumentNullException(nameof(args));
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null)
                {
                    throw new ArgumentNullException(nameof(args), $"Argument from index {i} cannot be null.");
                }
            }

            return string.Format(format, args);
        }

        public static string StringFormatUriEncode(string format, params string[] args)
        {
            var escapedArgs = new string[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                escapedArgs[i] = Uri.EscapeDataString(args[i]);
            }

            return StringFormatThrowNulls(format, escapedArgs);
        }

        // The 'parameters' parameter actually takes a anonymous object
        // (e.g. 'new { param1 = "value1", param2 = "value2" }')
        public static string BuildUrlQuery(object parameters)
        {
            var builder = new StringBuilder();
            foreach (var prop in parameters.GetType().GetProperties())
            {
                var value = prop.GetValue(parameters, null);

                if (value is bool b)
                {
                    value = b ? 1 : 0;
                }

                if (value != null)
                {
                    builder.Append($"{prop.Name}={Uri.EscapeDataString(value.ToString())}&");
                }
            }

            if (builder.Length > 0)
            {
                builder.Remove(builder.Length - 1, 1);
            }

            return builder.ToString();
        }

        // Helper for converting objects to multipart/form-data requests.
        // Avoids MultipartFormDataContent's asynchronous garbage.
        public static void WriteMultipartFormDataTo(Stream output, object obj, out string boundary)
        {
            boundary = "------------------------" + DateTime.Now.Ticks;

            using (var writer = new StreamWriter(output))
            {
                foreach (var prop in obj.GetType().GetProperties())
                {
                    var name = CamelCaseNaming.GetPropertyName(prop.Name, false);

                    // Content header
                    writer.WriteLine(boundary);
                    writer.WriteLine($"Content-Disposition: form-data; name=\"{name}\"");
                    writer.WriteLine();

                    if (prop.PropertyType == typeof(Stream))
                    {
                        var stream = prop.GetValue(obj) as Stream;
                        stream.CopyTo(output);
                    }
                    else if (prop.PropertyType == typeof(byte[]))
                    {
                        var bytes = prop.GetValue(obj) as byte[];
                        output.Write(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        writer.WriteLine(prop.GetValue(obj));
                    }
                }

                writer.Write(boundary + "--");
            }
        }
    }
}
