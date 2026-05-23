using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.DTOs.Tasks
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public TaskPriority Priority { get; set; }
            = TaskPriority.Medium;

        public Guid ProjectId { get; set; }
    }
}
