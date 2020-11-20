using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CodersAcademy.API.ViewModel.Request
{
	public class AlbumRequest : IValidatableObject
	{
		[Required]
		public String Name { get; set; }

		[Required]
		public String Band { get; set; }

		[Required]
		public String Description { get; set; }

		[Required]
		public String Backdrop { get; set; }

		[Required]
		public List<MusicRequest> Musics { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var result = new List<ValidationResult>();

			//Valida se Music não é nula
			if (this.Musics == null)
				yield return new ValidationResult("Album must contain at least one music.");

			//Valida se o objeto Music tem ao menos um item
			if (!this.Musics.Any())
				yield return new ValidationResult("Album must contain at least one music.");

			//Valida todas as propriedades do objeto Music
			foreach (var item in this.Musics)
			{
				if (!Validator.TryValidateObject(item, new ValidationContext(item), result))
					yield return result.First();
			}
			
		}
	}
}
