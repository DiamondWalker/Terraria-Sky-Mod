using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace SkyMod.Util {
    /**
     * Code derived from Stefan Gustavon's SimplexNoise1234 {https://github.com/stegu/perlin-noise/blob/master/src/simplexnoise1234.c}
    */
    public class SimplexNoise {
        byte[] perm = new byte[512];

        public SimplexNoise() {
            for (int i = 0; i < 256; i++) {
                perm[i] = (byte)i;
            }

            int index = 256;
            while (index > 1) {
                index--;
                int rand = Main.rand.Next(index + 1);
                byte swap = perm[rand];
                perm[rand] = perm[index];
                perm[index] = swap;
            }

            for (int i = 0; i < 256; i++) {
                perm[i + 256] = perm[i];
            }

            Console.Write(perm);
        }

        // 1D simplex noise
        public float Noise1D(float x) {
            int i0 = (int)Math.Floor(x);
            int i1 = i0 + 1;
            float x0 = x - i0;
            float x1 = x0 - 1.0f;

            float n0, n1;

            float t0 = 1.0f - x0*x0;
            //  if(t0 < 0.0f) t0 = 0.0f; // this never happens for the 1D case
            t0 *= t0;
            n0 = t0 * t0 * grad1(perm[i0 & 0xff], x0);

            float t1 = 1.0f - x1*x1;
            //  if(t1 < 0.0f) t1 = 0.0f; // this never happens for the 1D case
            t1 *= t1;
            n1 = t1 * t1 * grad1(perm[i1 & 0xff], x1);
            // The maximum value of this noise is 8*(3/4)^4 = 2.53125
            // A factor of 0.395 would scale to fit exactly within [-1,1], but
            // we want to match PRMan's 1D noise, so we scale it down some more.
            return 0.25f * (n0 + n1);

        }

        private const float F2 = 0.366025403f; // F2 = 0.5*(sqrt(3.0)-1.0)
        private const float G2 = 0.211324865f; // G2 = (3.0-Math.sqrt(3.0))/6.0

        // 2D simplex noise
        public float Noise2D(float x, float y) {
            float n0, n1, n2; // Noise contributions from the three corners

            // Skew the input space to determine which simplex cell we're in
            float s = (x+y)*F2; // Hairy factor for 2D
            float xs = x + s;
            float ys = y + s;
            int i = (int)Math.Floor(xs);
            int j = (int)Math.Floor(ys);

            float t = (float)(i+j)*G2;
            float X0 = i-t; // Unskew the cell origin back to (x,y) space
            float Y0 = j-t;
            float x0 = x-X0; // The x,y distances from the cell origin
            float y0 = y-Y0;

            // For the 2D case, the simplex shape is an equilateral triangle.
            // Determine which simplex we are in.
            int i1, j1; // Offsets for second (middle) corner of simplex in (i,j) coords
            if (x0>y0) { i1=1; j1=0; } // lower triangle, XY order: (0,0)->(1,0)->(1,1)
            else { i1=0; j1=1; }      // upper triangle, YX order: (0,0)->(0,1)->(1,1)

            // A step of (1,0) in (i,j) means a step of (1-c,-c) in (x,y), and
            // a step of (0,1) in (i,j) means a step of (-c,1-c) in (x,y), where
            // c = (3-sqrt(3))/6

            float x1 = x0 - i1 + G2; // Offsets for middle corner in (x,y) unskewed coords
            float y1 = y0 - j1 + G2;
            float x2 = x0 - 1.0f + 2.0f * G2; // Offsets for last corner in (x,y) unskewed coords
            float y2 = y0 - 1.0f + 2.0f * G2;

            // Wrap the integer indices at 256, to avoid indexing perm[] out of bounds
            int ii = i & 0xff;
            int jj = j & 0xff;

            // Calculate the contribution from the three corners
            float t0 = 0.5f - x0*x0-y0*y0;
            if (t0 < 0.0f) n0 = 0.0f;
            else {
                t0 *= t0;
                n0 = t0 * t0 * grad2(perm[ii+perm[jj]], x0, y0);
            }

            float t1 = 0.5f - x1*x1-y1*y1;
            if (t1 < 0.0f) n1 = 0.0f;
            else {
                t1 *= t1;
                n1 = t1 * t1 * grad2(perm[ii+i1+perm[jj+j1]], x1, y1);
            }

            float t2 = 0.5f - x2*x2-y2*y2;
            if (t2 < 0.0f) n2 = 0.0f;
            else {
                t2 *= t2;
                n2 = t2 * t2 * grad2(perm[ii+1+perm[jj+1]], x2, y2);
            }

            // Add contributions from each corner to get the final noise value.
            // The result is scaled to return values in the interval [-1,1].
            return 40.0f * (n0 + n1 + n2); // TODO: The scale factor is preliminary!
        }

        private float grad1(int hash, float x) {
            int h = hash & 15;
            float grad = 1.0f + (h & 7);   // Gradient value 1.0, 2.0, ..., 8.0
            if ((h & 8) != 0) grad = -grad;         // Set a random sign for the gradient
            return (grad * x);           // Multiply the gradient with the distance
        }

        private float grad2(int hash, float x, float y) {
            int h = hash & 7;      // Convert low 3 bits of hash code
            float u = h < 4 ? x : y;  // into 8 simple gradient directions,
            float v = h < 4 ? y : x;  // and compute the dot product with (x,y).
            return (((h & 1) != 0) ? -u : u) + (((h & 2) != 0) ? -2.0f * v : 2.0f * v);
        }
    }
}
