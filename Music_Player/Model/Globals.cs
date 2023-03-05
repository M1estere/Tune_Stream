namespace Music_Player.Model;

internal class Globals
{
    public static Random Random = new Random();

    public static void FillAllSongs()
    {
        AddSong("alive.mp3", "Warbly Jets", "warbly.png");
        AddSong("ancients.mp3", "Nier OST", "nier_replicant.png");
        AddSong("animals.mp3", "Maroon 5", "maroon_v.png");
        AddSong("ashes_of_dreams.mp3", "Nier OST", "nier_replicant.png");
        AddSong("bad.mp3", "Michael Jackson", "michael_jackson.jpg");

        AddSong("beat_it.mp3", "Michael Jackson", "michael_jackson.jpg");
        AddSong("believer.mp3", "Imagine Dragons", "imagine_dragons.jpg");
        AddSong("billie_jean.mp3", "Michael Jackson", "michael_jackson.jpg");
        AddSong("break_through_it_all.mp3", "Sonic OST", "sonic_frontiers_ost.png");
        AddSong("circles.mp3", "Post Malone", "post_malone.png");

        AddSong("cold_heart.mp3", "Elton John", "elton.png");
        AddSong("easier.mp3", "5SOS", "seconds_of_summer.png");
        AddSong("electrical_storm.mp3", "U2", "u2.png");
        AddSong("endless_possibility.mp3", "Sonic OST", "sonic_unleashed_ost.jpg");
        AddSong("feel_good_inc.mp3", "Gorillaz", "gorillaz.png");

        AddSong("get_lucky.mp3", "Daft Punk", "daft_punk.png");
        AddSong("grandmother.mp3", "Nier OST", "nier_replicant.png");
        AddSong("guy-exe.mp3", "Superfruit", "superfruit.png");
        AddSong("his_world.mp3", "Sonic OST", "sonic06_ost.png");
        AddSong("i_feel_it_coming.mp3", "The Weeknd", "weeknd.png");

        AddSong("in_your_eyes.mp3", "The Weeknd", "weeknd.png");
        AddSong("infinite.mp3", "Sonic OST", "sonic_forces_ost.png");
        AddSong("last_christmas.mp3", "WHAM!", "last_christmas.jpg");
        AddSong("lost_in_japan.mp3", "Shawn Mendes", "shawn_mendes.jpg");
        AddSong("makes_me_wonder.mp3", "Maroon 5", "maroon_v.png");

        AddSong("middle_of_a_breakup.mp3", "Panic! At The Disco", "patd_special.png");
        AddSong("misery.mp3", "Maroon 5", "maroon_v.png");
        AddSong("moves_like_jagger.mp3", "Maroon 5", "maroon_briefs.png");
        AddSong("pumped_up_kicks.mp3", "Foster The People", "foster.png");
        AddSong("radioactive.mp3", "Imagine Dragons", "imagine_dragons.jpg");

        AddSong("sacrifice.mp3", "The Weeknd", "the_weeknd_radio.jpg");
        AddSong("sad_clown.mp3", "Panic! At The Disco", "patd_regular.jpg");
        AddSong("save_yourself.mp3", "ONE OK ROCK", "oneokrock.jpg");
        AddSong("say_it_louder.mp3", "Panic! At The Disco", "patd_regular.jpg");
        AddSong("scared_of_the_dark.mp3", "Lil Wayne", "miles_morales.jpg");

        AddSong("shadowlord.mp3", "Nier OST", "nier_replicant.png");
        AddSong("stitches.mp3", "Shawn Mendes", "shawn_mendes.png");
        AddSong("sugar.mp3", "Maroon 5", "maroon_briefs.png");
        AddSong("sunflower.mp3", "Post Malone", "miles_morales.jpg");
        AddSong("the_world.mp3", "Nightmare", "the_world.jpg");

        AddSong("this_love.mp3", "Maroon 5", "maroon_v.png");
        AddSong("ultimate.mp3", "Thai McGrath", "sonic_frontiers_ultimate.jpg");
        AddSong("umbrella.mp3", "Rihanna", "rihanna.jpg");
        AddSong("undefeatable.mp3", "Sonic OST", "sonic_frontiers_ost.png");
        AddSong("vague_hope.mp3", "Nier OST", "nier_automata.png");

        AddSong("vandalize.mp3", "ONE OK ROCK", "oneokrock.jpg");
        AddSong("want_you_back.mp3", "5SOS", "seconds_of_summer.jpg");
        AddSong("what_lovers_do.mp3", "Maroon 5", "maroon_v.png");
        AddSong("whats_up_danger.mp3", "Blackway", "miles_morales.jpg");
        AddSong("world_adventure.mp3", "Sonic OST", "sonic_unleashed_ost.jpg");
    }

    private static void AddSong(string title, string author, string trackCover)
    {
        SongsInfo.AllSongs.Add(title);
        SongsInfo.AllAuthors.Add(author);
        SongsInfo.AllCovers.Add(trackCover);
    }
}
