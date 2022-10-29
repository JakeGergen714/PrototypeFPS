using System;

namespace util.axis
{
    public class Axis
    {
        private String name;

        private String positiveKey;
        private String negativeKey;

        public Axis(String name, String positiveKey, String negativeKey)
        {
            this.name = name;
            this.positiveKey = positiveKey;
            this.negativeKey = negativeKey;
        }

        public String getPositiveKey()
        {
            return this.positiveKey;
        }

        public String getNegativeKey()
        {
            return this.negativeKey;
        }
    }
}