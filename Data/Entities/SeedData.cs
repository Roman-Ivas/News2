using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Data.Entities
{
    public static class SeedData
    {
        public static async Task Initializy(IServiceProvider serviceProvider,
             IWebHostEnvironment webHost)
        {
            DbContextOptions<NewsContext> options = serviceProvider
                .GetRequiredService<DbContextOptions<NewsContext>>();
            using (NewsContext context = new NewsContext(options))
            {
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                if (context.News.Any()) return;

                byte[] image1 = File.ReadAllBytes($"{webHost.WebRootPath}\\img\\first.jpg");
                byte[] image2 = File.ReadAllBytes($"{webHost.WebRootPath}\\img\\secondary.jpg");
                byte[] image3 = File.ReadAllBytes($"{webHost.WebRootPath}\\img\\third.jpg");

                Authour authour1 = new Authour {AuthourName= "BBC England" };
                Authour authour2 = new Authour { AuthourName = "BBC Scotland" };

                await context.Authours.AddRangeAsync(authour1, authour2);

                News news1 = new News
                {
                    Authour = authour1,
                    //AuthorId = 1,
                    Title = "Plymouth shooting: Families say warning signs were ignored",
                    Description = "Breathtaking incompetence and failings by police allowed a gunman to kill five people during a mass shooting in Plymouth, victims' families have said.\r\n\r\nJake Davison killed his mother and four other people, including a girl aged three, with a shotgun in August 2021.\r\n\r\nFamilies of four of the victims said: \"Warning signs were ignored and a licence to kill was granted.\"\r\n\r\nThe inquest jury said there had been a \"catastrophic failure\" at Devon and Cornwall Police.\r\n\r\nAt the conclusion of a five-week inquest at Exeter Racecourse jurors said the deaths of the victims were \"caused by the fact the perpetrator had a legally-held shotgun\".\r\n\r\nAll five of the victims were unlawfully killed, the jury found.\r\n\r\nPlymouth shootings: Victims' loved ones tell their stories\r\nDavison killed his mother Maxine, 51, Sophie Martyn, three, her father Lee, 43, Stephen Washington, 59, and Kate Shepherd, 66, in the Keyham area of Plymouth before turning the gun on himself.\r\n\r\nThe Independent Office for Police Conduct (IOPC) said a criminal investigation into possible health and safety breaches by Devon and Cornwall Police was ongoing.",
                    Image = image1,
                    Date = new DateTime(2023, 2, 20)
                };
                News news2 = new News
                {
                    //AuthorId=1,
                    Authour = authour1,
                    Title= "King Charles watches Ukrainian troops training in Wiltshire",
                    Description= "King Charles III has said he is \"full of admiration\" for Ukrainian soldiers who are being trained by the British Army in Wiltshire.\r\n\r\nAround 20,000 Ukrainians are to be put through an intensive five-week course to help them prepare for combat.\r\n\r\nHis Majesty watched as the men - mostly civilians - stormed a mock trench.\r\n\r\n\"We're trying to get them to learn how to use the ground to their advantage,\" said Capt Freddie Bradshaw from 1 Irish Guards.\r\n\r\n\"We've got urban and trench complexes which they can use. They need to learn how to fight in a forest - a lot of Ukraine is forest.\r\n\r\n\"There's a huge amount of experience from the training teams here.\r\n\r\n\"We are trying to make them as lethal as possible in a short space of time - five weeks is a very condensed training package,\" he said.",
                    Image=image2,
                    Date = new DateTime(2023, 2, 19)
                };
                News news3 = new News {
                    Authour = authour2,
                    //AuthorId = 2,
                    Title = "Who will replace Nicola Sturgeon as next SNP leader?",
                    Description = "The health secretary is part of a newer generation of SNP figures, having become a Glasgow MSP in 2011.\r\n\r\nHe has held a number of senior posts in government, including as transport minister, Europe minister and justice secretary.\r\n\r\nAs the 37-year-old launched his campaign in Clydebank, he said he wanted to \"reenergise the campaign for independence\".\r\n\r\nHe said he had the experience to take on the job of first minister, but would have a \"a different approach\" to Nicola Sturgeon.\r\n\r\nShe had faced calls to sack Mr Yousaf over his running of the NHS in Scotland this winter, as waiting times hit record highs and doctors issued safety warnings.\r\n\r\nBut he has pointed to the pay offer made to NHS staff last week, which he says is likely to avoid strike action for the next financial year.\r\n\r\nHe has pitched himself as a candidate who would continue the work of Ms Sturgeon's administration and maintain the SNP's partnership government with the Greens.\r\n\r\nHe also says politics has grown too divisive, and that he has \"the skills to reach across the divide and bring people together\" across Scotland.\r\n\r\nIn terms of independence, he says he wants to talk about policy rather than process, and to \"grow our movement from the grassroots upwards\".\r\n\r\nProminent supporters: Neil Gray, international development minister; Maree Todd, public health minister; Michael Matheson, net zero, energy and transport secretary; Kevin Stewart, mental wellbeing and social care minister.",
                    Image = image3,
                    Date = new DateTime(2023, 2, 20)
                };
                await context.News.AddRangeAsync(news1,news2,news3);
                await context.SaveChangesAsync();
            }
        }
    }
}
