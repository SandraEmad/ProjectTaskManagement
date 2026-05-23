using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.DTOs.Tasks
{
    public class TaskDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ProjectTaskStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid ProjectId { get; set; }
    }
}
