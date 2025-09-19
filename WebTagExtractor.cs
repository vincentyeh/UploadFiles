using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

class WebTagExtractor
{
    static async Task Main(string[] args)
    {
        Console.Write("請輸入網址: ");
        string url = Console.ReadLine();

        Console.Write("請輸入要擷取的 tag 名稱 (例如 div, h1, p): ");
        string tagName = Console.ReadLine();

        var html = await DownloadHtmlAsync(url);
        if (html == null)
        {
            Console.WriteLine("下載失敗");
            return;
        }

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var nodes = doc.DocumentNode.SelectNodes($"//{tagName}");
        if (nodes == null)
        {
            Console.WriteLine($"找不到 {tagName} tag");
            return;
        }

        Console.WriteLine($"擷取到的 {tagName} tag 內容:");
        foreach (var node in nodes)
        {
            Console.WriteLine(node.InnerText.Trim());
        }
    }

    static async Task<string> DownloadHtmlAsync(string url)
    {
        try
        {
            using var client = new HttpClient();
            return await client.GetStringAsync(url);
        }
        catch
        {
            return null;
        }
    }
}
