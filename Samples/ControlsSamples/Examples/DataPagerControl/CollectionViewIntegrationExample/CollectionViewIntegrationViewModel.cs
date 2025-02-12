using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.DataPager;

namespace QSF.Examples.DataPagerControl.CollectionViewIntegrationExample;

public class CollectionViewIntegrationViewModel : ExampleViewModel
{
    private static readonly List<string> titles = new()
    {
        "Discoveries in weeks, not years: How AI and high-performance computing are speeding up scientific discovery",
        "India’s Myntra innovates with generative AI to help shoppers put the right look together",
        "Entrepreneurs are bringing new ideas and technologies to preserve the planet",
        "Accelerating Sustainability with AI: A Playbook",
        "As real as it gets: Pilots lend their expertise to the most authentic flight sim on the market",
        "Toyota is deploying AI agents to harness the collective wisdom of engineers and innovate faster",
        "Building a computer that solves practical problems at the speed of light",
        "Azure Quantum Elements aims to compress 250 years of chemistry into the next 25",
        "HoloLens 2 brings new immersive collaboration tools to industrial metaverse customers",
        "An easier way to help people give tech support to loved ones wins Microsoft Hackathon top prize",
        "Tech workers turn their skills toward helping the world — one disaster at a time",
        "More than 2 million Imagine Cup competitors change the world over 20 years of innovation",
        "MORSE security team takes proactive approach to finding bugs",
        "Microsoft's virtual datacenter grounds 'the cloud' in reality",
        "How technology is propelling nonprofits through a crisis to help even more people in need"
    };
    private static readonly List<string> descriptions = new()
    {
        "Computing has already accelerated scientific discovery. Now scientists say a combination of advanced AI with next-generation cloud computing is turbocharging the pace of discovery to speeds unimaginable just a few years ago.",
        "With absolutely no idea what they were looking for, this writer turned to an AI shopping assistant by Myntra, India’s biggest online fashion retailer, and typed, “I’m looking for clothes I can wear to work out in the gym.”",
        "Shana Fatina got the idea for her startup during a trip to Komodo National Park in Indonesia, where she enjoyed beaches, coral reefs and Komodo dragons, the park’s famous lizards.",
        "Given the urgency of the planetary crisis, society needs to push harder on the AI accelerator while establishing guardrails that steer the world safely, securely, and equitably toward net-zero emissions.",
        "The pilot had been missing for about a week, last seen taking off from an airport in Covington, Washington, heading home in his Piper Dakota to Puyallup, about 36 miles south of Seattle.",
        "Toyota City, Japan – Cars are going through some of the most rapid engineering shifts in their 100-year history, putting intense pressure on global automakers to innovate more quickly.",
        "Sometimes referred to as “the law of the instrument,” that hammer-and-nail idea is a common pitfall in research; when you’re not open to questioning your own methods, you might miss an opportunity for learning and impact.",
        "Catalysts that could recycle carbon dioxide into clean fuel. Compounds that could boost the power of batteries. Proteins that could transform how we treat disease.  ",
        "Even with precise written instructions, some parts of the automotive assembly process still require a bit of art, says David Kleiner, who leads Toyota Motor North America’s Applied Technology Research Lab.",
        "Seven strangers from different generations, cultures, jobs and even continents came together with a winning idea for Microsoft’s Global Hackathon when they discovered they’d all had the same experience",
        "Her children had grown up under frequent bombing in eastern Ukraine for the past eight years and would automatically — and calmly — run to an inner corridor for shelter whenever they heard the telltale whoosh.",
        "Two university students stuck in pandemic lockdown during a semester abroad fell in love — and then came up with an idea that could change the way scientists and engineers around the world conduct their research.",
        "When it comes to a complex issue such as computer security, there are no simple answers. As the effects of hacking run the gamut from the annoyingly personal – like never-ending popup windows on your computer screen.",
        "It’s where remote learners and workers gather for virtual lessons and meetings, where gamers converge to build worlds, race cars and blast away foes, and where healthcare workers have been keeping track of COVID-19 patients and the resources they need to treat them..",
        "A Native American Youth and Family Center advocate helped Nelson, a member of a Sioux tribe, sign up on a new platform called My NeighbOR.",
    };
    private static readonly string iconListView = "\ue8bc";
    private static readonly string iconGridView = "\ue8bb";
    private readonly ObservableCollection<TechnologyNews> news = new ObservableCollection<TechnologyNews>();
    private DataPagerDisplayMode displayMode;
    private string icon;
    private int spanCount;

    public CollectionViewIntegrationViewModel()
    {
        this.AddNews();
        this.AddNews();

        this.displayMode = DataPagerDisplayMode.FirstPageButton |
                           DataPagerDisplayMode.PrevPageButton |
                           DataPagerDisplayMode.NumericButtons |
                           DataPagerDisplayMode.NavigationComboBox |
                           DataPagerDisplayMode.NextPageButton |
                           DataPagerDisplayMode.LastPageButton |
                           DataPagerDisplayMode.NavigationView;

        this.ChangeLayoutCommand = new Command(this.OnChangeLayoutCommandExecuted);
        this.SetGridViewLayout();
    }

    public Command ChangeLayoutCommand { get; set; }

    public ObservableCollection<TechnologyNews> News { get => this.news; }

    public DataPagerDisplayMode DisplayMode { get => this.displayMode; }

    public string Icon { get => this.icon; set => this.UpdateValue(ref this.icon, value); }

    public int SpanCount { get => this.spanCount; set => this.UpdateValue(ref this.spanCount, value); }

    private void AddNews()
    {
        for (int i = 1; i <= 15; i++)
        {
            this.news.Add(new TechnologyNews()
            {
                Image = $"tech_news_{i}.jpg",
                Title = titles[i - 1],
                Description = descriptions[i - 1],
            });
        }
    }

    private void OnChangeLayoutCommandExecuted(object obj)
    {
        if (this.icon == iconListView)
        {
            this.SetListViewLayout();
        }
        else
        {
            this.SetGridViewLayout();
        }
    }

    private void SetGridViewLayout()
    {
        this.Icon = iconListView;

#if ANDROID || IOS
        this.SpanCount = 2;
#elif MACCATALYST || WINDOWS
        this.SpanCount = 3;
#endif
    }

    private void SetListViewLayout()
    {
        this.Icon = iconGridView;
        this.SpanCount = 1;
    }
}