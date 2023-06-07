using System;
using System.Collections.Generic;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public static class ParticipantLines
{
    private static readonly List<string> lines;
    private static readonly Random random;

    static ParticipantLines()
    {
        random = new Random();

        lines = new List<string>
        {
            "Did you know that if you boil an egg in a microwave it is going to explode?",
            "Do you mean that the microwave will explode?",
            "No, I did not know that!",
            "Hi there, friend!",
            "Has anyone seen my phone? I think I might have lost it.",
            "What do you mean?",
            "Do you want to hear a programmer joke?",
            "Who's there?",
            "I think this is an interesting idea : )",
            "Show me!",
            "... even if you could pretend ...",
            "nope",
            "No! No! No!",
            "Lorem ipsum?",
            "Ok, let me explain in more details.",
            "So, you take an egg. Right? It's simple to understand.",
            "true true",
            "tl;dr;",
            "The silence is loud and clear, TV Girl",
            "How many developers do you need to change a lightbulb?",
            "None! Because it is a hardware problem. ha!",
            "Come join the ceremony",
            "Did you see the new Zack Snyder movie? it's so cooool",
            "I can't believe you guys :D",
            "Sounds good, put some more emojis there :)",
            "ok then :|",
            "Who came up with that idea :?",
        };
    }

    public static string GetRandomLine()
    {
        int index = random.Next(0, lines.Count);
        return lines[index];
    }
}
