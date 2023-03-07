﻿using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "o nome é obrigatório")]
        [StringLength(40,MinimumLength = 3, ErrorMessage = "Este campo deve conter entre 3 e 40 caracteres ")]
        //[EmailAddress]
       // [Range]
        public string Name { get; set; }
        [Required(ErrorMessage = "o slug é obrigatório")]
        public string Slug { get; set; }


    }
}
