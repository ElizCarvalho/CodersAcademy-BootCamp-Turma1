﻿using System;
using System.Collections.Generic;

namespace CodersAcademy.API.ViewModel.Response
{
	public class UserResponse
	{
		public Guid Id { get; set; }
		public String Name { get; set; }
		public String Email { get; set; }
		public String Photo { get; set; }
		public IList<FavoriteMusicResponse> FavoriteMusics { get; set; }
	}
}
