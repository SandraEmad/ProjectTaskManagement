

namespace ProjectTaskManagement.Domain.Entities
{
    public class Project
    {
      
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        //public Guid UserId { get; set; }
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        public ICollection<ProjectTask> Tasks { get; set; }
            = new List<ProjectTask>();
    }
}
