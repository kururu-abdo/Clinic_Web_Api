namespace clinic.Dto
{

    public class AddScheduleDto
    {
        public DateTime SheduleDate {get; set;}

                public Guid DoctorId {get; set;}
                public Guid SheduleTypeId {get; set;}

    }
    
}