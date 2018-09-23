namespace Yaw{
    public class YawAngle
    {
        private double _apparentWindAngle;
        private double _apparentWindSpeed;

        public double ApparentWindAngle => _apparentWindAngle;

        public double ApparentWindSpeed => _apparentWindSpeed;

        public void CalculateApparentWindSpeed(double windSpeed, double riderSpeed, double trueAngle)
        {

            _apparentWindSpeed = GetApparentWindSpeed(windSpeed, riderSpeed, ConvertToRadians(trueAngle));
            _apparentWindAngle = GetApparentWindAngle(trueAngle, windSpeed, riderSpeed);
        }
        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private double GetApparentWindSpeed(double windSpeed, double riderSpeed, double lambda)
        {
            return Math.Sqrt(Math.Pow(windSpeed, 2) + Math.Pow(riderSpeed, 2) + 2 * windSpeed * riderSpeed * Math.Cos(lambda));
        }

        private double GetApparentWindAngle(double trueAngle, double windSpeed, double riderSpeed)
        {

            int angle;
            var sign = 1; // -1 for left, +1 for right
            if (trueAngle < 0)
            {
                sign = -1;
                trueAngle *= -1;
            }
            angle = Deg(Math.Atan(windSpeed * Math.Sin(ConvertToRadians(trueAngle)) / (riderSpeed + windSpeed * Math.Cos(ConvertToRadians(trueAngle)))));

            if (angle < 0)
            {
                angle = angle + 180;
            }
            return angle * sign;
        }

        private int Deg(double x)
        {
            return Convert.ToInt32((x / Math.PI) * 180);
        }
    }
}
