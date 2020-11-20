using System;
using System.ComponentModel.DataAnnotations;

namespace CodersAcademy.API.ViewModel.Request
{
	public class AlbumRequest
	{
		[Required]
		public String Name { get; set; }

		[Required]
		public String Band { get; set; }

		[Required]
		public String Description { get; set; }

		[Required]
		public String Backdrop { get; set; }
	}
}
