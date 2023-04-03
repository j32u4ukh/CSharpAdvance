using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSharpAdvance
{
    class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    class Comment
    {
        public int PostId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }

    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            //// GET /posts
            //var posts = await GetPosts();
            //Console.WriteLine("All Posts:");
            //foreach (var post in posts)
            //{
            //    Console.WriteLine($"Title: {post.Title}");
            //}

            //// GET /posts/1
            //var post1 = await GetPostById(1);
            //Console.WriteLine("Post 1:");
            //Console.WriteLine($"Title: {post1.Title}");

            //// GET /posts/1/comments
            //var comments = await GetCommentsByPostId(1);
            //Console.WriteLine("Comments of Post 1:");
            //foreach (var comment in comments)
            //{
            //    Console.WriteLine($"Name: {comment.Name}, Email: {comment.Email}, Body: {comment.Body}");
            //}

            //// GET /comments?postId=1
            //var commentsOfPost1 = await GetCommentsByPostIdQuery(1);
            //Console.WriteLine("Comments of Post 1 (Query):");
            //foreach (var comment in commentsOfPost1)
            //{
            //    Console.WriteLine($"Name: {comment.Name}, Email: {comment.Email}, Body: {comment.Body}");
            //}

            //await PostAsync();
            //await PutAsync();
            await PatchAsync();
            //await DeleteAsync();

            Console.ReadKey();
        }

        static async Task<List<Post>> GetPosts()
        {
            var responseString = await client.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
            var posts = JsonConvert.DeserializeObject<List<Post>>(responseString);
            return posts;
        }

        static async Task<Post> GetPostById(int id)
        {
            var responseString = await client.GetStringAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
            var post = JsonConvert.DeserializeObject<Post>(responseString);
            return post;
        }

        static async Task<List<Comment>> GetCommentsByPostId(int postId)
        {
            var responseString = await client.GetStringAsync($"https://jsonplaceholder.typicode.com/posts/{postId}/comments");            
            var comments = JsonConvert.DeserializeObject<List<Comment>>(responseString);
            return comments;
        }

        static async Task<List<Comment>> GetCommentsByPostIdQuery(int postId)
        {
            var responseString = await client.GetStringAsync($"https://jsonplaceholder.typicode.com/comments?postId={postId}");
            var comments = JsonConvert.DeserializeObject<List<Comment>>(responseString);
            return comments;
        }

        static async Task PostAsync()
        {
            var post = new Post
            {
                UserId = 1,
                Title = "Test Post",
                Body = "This is a test post."
            };

            var postJson = JsonConvert.SerializeObject(post);

            var content = new StringContent(postJson, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://jsonplaceholder.typicode.com/posts", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var createdPost = JsonConvert.DeserializeObject<Post>(responseString);

            Console.WriteLine($"Created post:");
            Console.WriteLine($"UserId: {createdPost.UserId}");
            Console.WriteLine($"Id: {createdPost.Id}");
            Console.WriteLine($"Title: {createdPost.Title}");
            Console.WriteLine($"Body: {createdPost.Body}");
        }

        static async Task PutAsync()
        {
            var post = new Post
            {
                UserId = 1,
                Id = 1,
                Title = "Updated Test Post",
                Body = "This is an updated test post."
            };

            var postJson = JsonConvert.SerializeObject(post);

            var content = new StringContent(postJson, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://jsonplaceholder.typicode.com/posts/1", content);

            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"PUT response: {responseString}");
        }

        static async Task PatchAsync()
        {
            // 設定Http請求方法為PATCH
            var method = new HttpMethod("PATCH");

            var post = new Post
            {
                Title = "Patched Test Post"
            };

            var postJson = JsonConvert.SerializeObject(post);

            var content = new StringContent(postJson, System.Text.Encoding.UTF8, "application/json");

            // 建立Http請求訊息
            var request = new HttpRequestMessage(method, "https://jsonplaceholder.typicode.com/posts/1")
            {
                Content = content
            };

            // 執行Http請求
            HttpResponseMessage response = await client.SendAsync(request);

            // 讀取Http回應內容
            string responseContent = await response.Content.ReadAsStringAsync();

            // 顯示Http回應內容
            Console.WriteLine(responseContent);

            Console.WriteLine($"PATCH response: {responseContent}");
        }

        static async Task DeleteAsync()
        {
            var response = await client.DeleteAsync("https://jsonplaceholder.typicode.com/posts/1");

            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"DELETE response: {responseString}");
        }
    }
}
