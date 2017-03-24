using System;

namespace SpaData.Models
{
    /// <summary>
    /// Entity model for Person
    /// </summary>
    public class Person
    {
        #region Properties

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        #endregion

        #region Constructors
        //TODO: Set CreatedBy dynamically
        public Person()
        {
            IsActive = true;
            CreatedBy = 0L;
            CreatedOn = DateTime.Now;
        }
        #endregion
    }
}
