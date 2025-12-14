using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace api.DTOs.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage="Title must be 3 characters")]
        [MaxLength(280, ErrorMessage="Title must over 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage="Content must be 3 characters")]
        [MaxLength(280, ErrorMessage="Content must over 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}