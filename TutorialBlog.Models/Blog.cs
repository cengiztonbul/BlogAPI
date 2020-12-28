using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TutorialBlog.Models
{
	public class Blog
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		public string Author { get; set; }

		public string BlogTitle { get; set; }

		public string BlogText { get; set; }

		public DateTime BlogDate { get; set; }
	}
}
