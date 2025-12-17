using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Telerik.AppUtils.Services;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public static class ParticipantLines
{
    private static readonly List<string> lines;
    private static Random random;

    static ParticipantLines()
    {
        random = DependencyService
              .Get<ITestingService>()
              .Random(10);
        lines = new List<string>
        {
            "Good afternoon, everyone! 👋",
            "Welcome to the discussion—let’s get started. 🚀",
            "Thanks for joining the chat today.",
            "How’s everyone doing? :)",
            "Let me know if you have any questions.",
            "Could you elaborate on that point?",
            "What’s the main goal you’re aiming for? :?",
            "Can you share an example?",
            "Would you mind explaining that in simpler terms?",
            "What’s your perspective on this?",
            "That’s an interesting observation. 💡",
            "I completely agree with your point.",
            "Thanks for clarifying that.",
            "I see what you mean now. :)",
            "Let's do this! 💪",
            "That makes perfect sense!",
            "Hey George, can we merge this last PR please? :?",
            "Here’s a possible approach we could take.",
            "Let’s consider an alternative solution.",
            "What if we try a different angle?",
            "I suggest we prioritize the key tasks first.",
            "Maybe we can break this down into smaller steps.",
            "Have you reviewed the latest documentation?",
            "The API update looks promising.",
            "Let’s check the system logs for more details.",
            "We should validate the data before deployment.",
            "Testing is critical before we push to production.",
            "Great work on that proposal! 🎉",
            "Thanks for your input—it’s valuable. 🙌",
            "Let’s keep the momentum going.",
            "I have super safe change that needs to be included for the upcoming release. Any objections?",
            "We’re almost there—just a few more steps. ⏳",
            "Looking forward to seeing the final results. 👏"
        };
    }

    public static string GetRandomLine()
    {
        int index = random.Next(0, lines.Count);
        return lines[index];
    }
}
