using Testing_Poc_Healthcare.Interface;

namespace Testing_Poc_Healthcare.Models
{
    public class PatientResponse : ResponseStatus, IData<int>
    {
        public int Data { get; set; }
    }
}
