namespace Othaura {

    using System.Collections.Generic;
    using UnityEngine;

    public class Console {

        private const int LOG_LINES = 128;
        private const int LOG_LINE_WIDTH = 256;

        private static Vector2i mSize;
        private static LinkedList<FastString> mLog;

        public static void Initialize(Vector2i size) {

            mLog = new LinkedList<FastString>();

            mSize = size;
            for (int i = 0; i < LOG_LINES; i++) {

                mLog.AddLast(new FastString(LOG_LINE_WIDTH));
            }
        }

        public static void Log(FastString str) {

            // Rotate a log line from the end of the log to the start
            var line = mLog.Last;
            mLog.RemoveLast();

            line.Value.Clear().Append(str);

            mLog.AddFirst(line.Value);
        }

        public static void Render() {

            int yOffset = 0;
            var curLine = mLog.First;

            // Keep looping and printing log lines until we the console max height
            // TODO: Needs more work to support multi-line log lines
            while (yOffset < mSize.height && curLine != null && curLine.Value.Length > 0) {
                
                var lineSize = RB.PrintMeasure(new Rect2i(0, 0, mSize.width, 9999), RB.TEXT_OVERFLOW_WRAP, curLine.Value);

                // Draw drop shadow
                RB.Print(new Rect2i(4, RB.DisplaySize.height - yOffset - 4 - 8 + 1, mSize.width, 9999), Color.black, RB.TEXT_OVERFLOW_WRAP | RB.NO_INLINE_COLOR, curLine.Value);

                // Draw text
                RB.Print(new Rect2i(4, RB.DisplaySize.height - yOffset - 4 - 8, mSize.width, 9999), Color.white, RB.TEXT_OVERFLOW_WRAP, curLine.Value);

                yOffset += lineSize.height + 2;

                curLine = curLine.Next;
            }
        }
    }
}

