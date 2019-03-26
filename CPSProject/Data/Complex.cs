﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Data
{
    public class Complex
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public static Complex Add(Complex a, Complex b)
        {
            Complex result = new Complex();
            result.Real = a.Real + b.Real;
            result.Imaginary = a.Imaginary + b.Imaginary;
            return result;
        }

        public static Complex Subtract(Complex a, Complex b)
        {
            Complex result = new Complex();
            result.Real = a.Real - b.Real;
            result.Imaginary = a.Imaginary - b.Imaginary;
            return result;
        }

        public static Complex Multiply(Complex a, Complex b)
        {
            Complex result = new Complex();
            result.Real = a.Real * b.Real + a.Imaginary * b.Imaginary;
            result.Imaginary = a.Real * b.Imaginary + a.Imaginary * b.Real;
            return result;
        }

        public static Complex Divide(Complex a, Complex b)
        {
            Complex result = new Complex();
            result = Multiply(a, b);
            double divisor = b.Real * b.Real + b.Imaginary * b.Imaginary;
            if(divisor >= 0.001 || divisor <= -0.001)
            {
                result.Real /= divisor;
                result.Imaginary /= divisor;
            }
            else
            {
                result.Real = 0;
                result.Imaginary = 0;
            }
            return result;
        }
    }
}
