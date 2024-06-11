using System.ComponentModel.DataAnnotations;

namespace Kafu.Model.ViewModel
{
   public class LoginViewModel
    {
            /// <summary>
            /// Gets or sets the userName.
            /// </summary>
            /// <value>The userName.</value>
            [Required]
            [Display(Name = "UserName")]
          
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the password.
            /// </summary>
            /// <value>The password.</value>
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [remember me].
            /// </summary>
            /// <value><c>true</c> if [remember me]; otherwise, <c>false</c>.</value>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
    }
