namespace EthanKennerly.PoorLife
{
    public class step_log
    {
        public int index { get; set; }
        public LogStep log_step { get; set; }
        public int parameter { get; set; }

        public override string ToString()
        {
            return index + "," + log_step + "," + parameter;
        }
    }
}
