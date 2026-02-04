using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class RyhmanHighScoret : MonoBehaviour
{


    /*
     * Netissä Nimimerkki on tallessa lempiNimi muuttujassa
     * 
     */
    public string url = "https://loruhighscore.ew.r.appspot.com";
    public string port;
    public bool localtest;
    public TextMeshProUGUI pelaajanNimiTeksti;
    public List<TextMeshProUGUI> parhaatTekstit;


    public static string pelaajanNimimerkki;

    public static string ryhmaId;
    public static string email = "";

    public GameObject nameInput;


    public static ParhaatPisteetMalli ladatutParhaatPisteet;


    public static RyhmanHighScoret instance;

    void Awake()
    {
        MakeSingleton();

        void MakeSingleton()
        {
            if (instance == null)
            {
                instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            else if (instance != null)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {

        if (localtest)
        {
            url = "http://localhost:7000";
        }

        // wait for TehtavienKartoitus.Kartoita() to finish then AsetaParhaaPisteet()
        var tehtavatKartoitus = FindObjectOfType<TehtavatKartoitus>();
        if (tehtavatKartoitus != null)
        {
            AsetaParhaatPisteet();
        }


    }

    public void OnKartoitusValmis()
    {
        AsetaParhaatPisteet(); // Call this when the event is raised
    }

    public static void AsetaParhaatPisteet()
    {

        print("user email is: " + email);
        if (email == null || email == "" || email == "anonymous@anom.com")
        {
            string[] animals = { "Canidae", "Felidae",  "Cat",  "Cattle",   "Dog",  "Donkey",   "Goat", "Guinea pig",   "Horse",    "Pig",  "Rabbit",   "Fancy rat varieties",  "laboratory rat strains",   "Sheep breeds", "Water buffalo breeds", "Chicken breeds",   "Duck breeds",  "Goose breeds", "Pigeon breeds",    "Turkey breeds",    "Aardvark", "Aardwolf", "African buffalo",  "African elephant", "African leopard",  "Albatross",    "Alligator",    "Alpaca",   "American buffalo (bison)", "American robin",   "Amphibian",    "list", "Anaconda", "Angelfish",    "Anglerfish",   "Ant",  "Anteater", "Antelope", "Antlion",  "Ape",  "Aphid",    "Arabian leopard",  "Arctic Fox",   "Arctic Wolf",  "Armadillo",    "Arrow crab",   "Asp",  "Ass (donkey)", "Baboon",   "Badger",   "Bald eagle",   "Bandicoot",    "Barnacle", "Barracuda",    "Basilisk", "Bass", "Bat",  "Beaked whale", "Bear", "list", "Beaver",   "Bedbug",   "Bee",  "Beetle",   "Bird", "list", "Bison",    "Blackbird",    "Black panther",    "Black widow spider",   "Blue bird",    "Blue jay", "Blue whale",   "Boa",  "Boar", "Bobcat",   "Bobolink", "Bonobo",   "Booby",    "Box jellyfish",    "Bovid",    "Buffalo, African", "Buffalo, American (bison)",    "Bug",  "Butterfly",    "Buzzard",  "Camel",    "Canid",    "Cape buffalo", "Capybara", "Cardinal", "Caribou",  "Carp", "Cat",  "list", "Catshark", "Caterpillar",  "Catfish",  "Cattle",   "list", "Centipede",    "Cephalopod",   "Chameleon",    "Cheetah",  "Chickadee",    "Chicken",  "list", "Chimpanzee",   "Chinchilla",   "Chipmunk", "Clam", "Clownfish",    "Cobra",    "Cockroach",    "Cod",  "Condor",   "Constrictor",  "Coral",    "Cougar",   "Cow",  "Coyote",   "Crab", "Crane",    "Crane fly",    "Crawdad",  "Crayfish", "Cricket",  "Crocodile",    "Crow", "Cuckoo",   "Cicada",   "Damselfly",    "Deer", "Dingo",    "Dinosaur", "list", "Dog",  "list", "Dolphin",  "Donkey",   "list", "Dormouse", "Dove", "Dragonfly",    "Dragon",   "Duck", "list", "Dung beetle",  "Eagle",    "Earthworm",    "Earwig",   "Echidna",  "Eel",  "Egret",    "Elephant", "Elephant seal",    "Elk",  "Emu",  "English pointer",  "Ermine",   "Falcon",   "Ferret",   "Finch",    "Firefly",  "Fish", "Flamingo", "Flea", "Fly",  "Flyingfish",   "Fowl", "Fox",  "Frog", "Fruit bat",    "Gamefowl", "list", "Galliform",    "list", "Gazelle",  "Gecko",    "Gerbil",   "Giant panda",  "Giant squid",  "Gibbon",   "Gila monster", "Giraffe",  "Goat", "list", "Goldfish", "Goose",    "list", "Gopher",   "Gorilla",  "Grasshopper",  "Great blue heron", "Great white shark",    "Grizzly bear", "Ground shark", "Ground sloth", "Grouse",   "Guan", "list", "Guanaco",  "Guineafowl",   "list", "Guinea pig",   "list", "Gull", "Guppy",    "Haddock",  "Halibut",  "Hammerhead shark", "Hamster",  "Hare", "Harrier",  "Hawk", "Hedgehog", "Hermit crab",  "Heron",    "Herring",  "Hippopotamus", "Hookworm", "Hornet",   "Horse",    "list", "Hoverfly", "Hummingbird",  "Humpback whale",   "Hyena",    "Iguana",   "Impala",   "Irukandji jellyfish",  "Jackal",   "Jaguar",   "Jay",  "Jellyfish",    "Junglefowl",   "Kangaroo", "Kangaroo mouse",   "Kangaroo rat", "Kingfisher",   "Kite", "Kiwi", "Koala",    "Koi",  "Komodo dragon",    "Krill",    "Ladybug",  "Lamprey",  "Landfowl", "Land snail",   "Lark", "Leech",    "Lemming",  "Lemur",    "Leopard",  "Leopon",   "Limpet",   "Lion", "Lizard",   "Llama",    "Lobster",  "Locust",   "Loon", "Louse",    "Lungfish", "Lynx", "Macaw",    "Mackerel", "Magpie",   "Mammal",   "Manatee",  "Mandrill", "Manta ray",    "Marlin",   "Marmoset", "Marmot",   "Marsupial",    "Marten",   "Mastodon", "Meadowlark",   "Meerkat",  "Mink", "Minnow",   "Mite", "Mockingbird",  "Mole", "Mollusk",  "Mongoose", "Monitor lizard",   "Monkey",   "Moose",    "Mosquito", "Moth", "Mountain goat",    "Mouse",    "Mule", "Muskox",   "Narwhal",  "Newt", "New World quail",  "Nightingale",  "Ocelot",   "Octopus",  "Old World quail",  "Opossum",  "Orangutan",    "Orca", "Ostrich",  "Otter",    "Owl",  "Ox",   "Panda",    "Panther",  "Panthera hybrid",  "Parakeet", "Parrot",   "Parrotfish",   "Partridge",    "Peacock",  "Peafowl",  "Pelican",  "Penguin",  "Perch",    "Peregrine falcon", "Pheasant", "Pig",  "Pigeon",   "list", "Pike", "Pilot whale",  "Pinniped", "Piranha",  "Planarian",    "Platypus", "Polar bear",   "Pony", "Porcupine",    "Porpoise", "Portuguese man o' war",    "Possum",   "Prairie dog",  "Prawn",    "Praying mantis",   "Primate",  "Ptarmigan",    "Puffin",   "Puma", "Python",   "Quail",    "Quelea",   "Quokka",   "Rabbit",   "list", "Raccoon",  "Rainbow trout",    "Rat",  "Rattlesnake",  "Raven",    "Ray (Batoidea)",   "Ray (Rajiformes)", "Red panda",    "Reindeer", "Reptile",  "Rhinoceros",   "Right whale",  "Roadrunner",   "Rodent",   "Rook", "Rooster",  "Roundworm",    "Saber-toothed cat",    "Sailfish", "Salamander",   "Salmon",   "Sawfish",  "Scale insect", "Scallop",  "Scorpion", "Seahorse", "Sea lion", "Sea slug", "Sea snail",    "Shark",    "list", "Sheep",    "list", "Shrew",    "Shrimp",   "Silkworm", "Silverfish",   "Skink",    "Skunk",    "Sloth",    "Slug", "Smelt",    "Snail",    "Snake",    "list", "Snipe",    "Snow leopard", "Sockeye salmon",   "Sole", "Sparrow",  "Sperm whale",  "Spider",   "Spider monkey",    "Spoonbill",    "Squid",    "Squirrel", "Starfish", "Star-nosed mole",  "Steelhead trout",  "Stingray", "Stoat",    "Stork",    "Sturgeon", "Sugar glider", "Swallow",  "Swan", "Swift",    "Swordfish",    "Swordtail",    "Tahr", "Takin",    "Tapir",    "Tarantula",    "Tarsier",  "Tasmanian devil",  "Termite",  "Tern", "Thrush",   "Tick", "Tiger",    "Tiger shark",  "Tiglon",   "Toad", "Tortoise", "Toucan",   "Trapdoor spider",  "Tree frog",    "Trout",    "Tuna", "Turkey",   "list", "Turtle",   "Tyrannosaurus",    "Urial",    "Vampire bat",  "Vampire squid",    "Vicuna",   "Viper",    "Vole", "Vulture",  "Wallaby",  "Walrus",   "Wasp", "Warbler",  "Water Boa",    "Water buffalo",    "Weasel",   "Whale",    "Whippet",  "Whitefish",    "Whooping crane",   "Wildcat",  "Wildebeest",   "Wildfowl", "Wolf", "Wolverine",    "Wombat",   "Woodpecker",   "Worm", "Wren", "Xerinae",  "X-ray fish",   "Yak",  "Yellow perch", "Zebra",    "Zebra finch",  "Animals by number of neurons", "Animals by size",  "Common household pests",   "Common names of poisonous animals",    "Alpaca",   "Bali cattle",  "Cat",  "Cattle",   "Chicken",  "Dog",  "Domestic Bactrian camel",  "Domestic canary",  "Domestic dromedary camel", "Domestic duck",    "Domestic goat",    "Domestic goose",   "Domestic guineafowl",  "Domestic hedgehog",    "Domestic pig", "Domestic pigeon",  "Domestic rabbit",  "Domestic silkmoth",    "Domestic silver fox",  "Domestic turkey",  "Donkey",   "Fancy mouse",  "Fancy rat",    "Lab rat",  "Ferret",   "Gayal",    "Goldfish", "Guinea pig",   "Guppy",    "Horse",    "Koi",  "Llama",    "Ringneck dove",    "Sheep",    "Siamese fighting fish",    "Society finch",    "Yak",  "Water buffalo"
            };
            string[] colors = { "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Magenta", "Purple", "White", "Black", "Gray", "Silver", "Pink", "Maroon", "Brown", "Beige", "Tan", "Peach", "Lime", "Olive", "Turquoise", "Teal", "Indigo", "Violet"
            };
            pelaajanNimimerkki = colors[UnityEngine.Random.Range(0, colors.Length - 1)] + animals[UnityEngine.Random.Range(0, animals.Length - 1)];

            //Vanha nimenkirjoitus input, aiheutti ikävää sisältöogelmaa, huonoja nimimerkkejä
            //RyhmanHighScoret.instance.nameInput.SetActive(true);
        }
        else
        {
            pelaajanNimimerkki = email.Substring(0, email.IndexOf("@"));
            if (pelaajanNimimerkki.Contains("."))
            {
                pelaajanNimimerkki = pelaajanNimimerkki.Substring(0, pelaajanNimimerkki.IndexOf("."));
            }
            string tmpLoppu = pelaajanNimimerkki.Substring(1);
            string ekaKirjain = pelaajanNimimerkki[0].ToString();
            ekaKirjain = ekaKirjain.ToUpper();
            pelaajanNimimerkki = ekaKirjain + tmpLoppu;

            LahetaPisteet();


        }
        RyhmanHighScoret.instance.pelaajanNimiTeksti.text = pelaajanNimimerkki;
        LahetaPisteet();

    }


    public static void LahetaPisteet()
    {
        int haePaikallisetPisteet = PlayerPrefs.GetInt(Pelikohtaiset.pelinNimi + "ParhaatPisteet" + ryhmaId);
        RyhmanHighScoret.instance.StartCoroutine(RyhmanHighScoret.instance.Laheta(pelaajanNimimerkki,
            ryhmaId,
            Pelikohtaiset.pelinNimi, haePaikallisetPisteet
            ));

    }

    [Serializable]
    public class PisteData
    {
        public string lempiNimi;
        public int parhaatPisteet;
    }

    [Serializable]
    public class ParhaatPisteetMalli
    {
        public string _id;
        public string ryhmaId;
        public string peliNimi;
        public List<PisteData> pisteData = new List<PisteData>();
    }



    public IEnumerator Laheta(string pelaajanNimimerkki, string ryhmaID, string peliNimi, int parhaatPisteet)
    {

        ParhaatPisteetMalli lahetettava = new ParhaatPisteetMalli();

        lahetettava.ryhmaId = ryhmaID;
        lahetettava.peliNimi = peliNimi;
        PisteData pisteetLahetykseen = new PisteData();
        pisteetLahetykseen.lempiNimi = pelaajanNimimerkki;
        pisteetLahetykseen.parhaatPisteet = parhaatPisteet;  //parhaatPisteet;
        lahetettava.pisteData.Add(pisteetLahetykseen);
        string json = JsonUtility.ToJson(lahetettava);
        var lahetys = new UnityWebRequest(url + "/pisteet", "POST");
        //lahetys.chunkedTransfer = false;
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        lahetys.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        lahetys.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        lahetys.SetRequestHeader("Content-Type", "application/json");


        //Send the request then wait here until it returns
        yield return lahetys.SendWebRequest();
        lahetys.uploadHandler.Dispose();
        if (lahetys.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + lahetys.error);
        }
        else
        {
            //vastaanotan vastauksen
            ladatutParhaatPisteet = JsonUtility.FromJson<ParhaatPisteetMalli>(lahetys.downloadHandler.text);
            PisteData paikallisetPisteet = new PisteData();
            paikallisetPisteet.lempiNimi = pelaajanNimimerkki;
            paikallisetPisteet.parhaatPisteet = parhaatPisteet;

            List<string> nimiMerkitNetista = new List<string>();

            //monesko tallenne on tämän pelaajan
            int index = 0;
            foreach (var item in ladatutParhaatPisteet.pisteData)
            {
                nimiMerkitNetista.Add(item.lempiNimi);
                if (item.lempiNimi == pelaajanNimimerkki)
                {
                    index = ladatutParhaatPisteet.pisteData.IndexOf(item);
                }
            }

            // jos nimimerkillä löytyy jo pelaajan parhaat pisteet
            if (nimiMerkitNetista.Contains(pelaajanNimimerkki))
            {
                int parasPelaajanPisteServerilla = ladatutParhaatPisteet.pisteData[index].parhaatPisteet;
                int parasPelaajanPisteNyt = parhaatPisteet;
                //Debug.Log("serv" + parasPelaajanPisteServerilla);
                //Debug.Log("nytte" + parasPelaajanPisteNyt);
                //onko paikalliset pisteet parempi kuin lokaali
                if (parasPelaajanPisteServerilla < parasPelaajanPisteNyt)
                {
                    // serverillä on jo pisteet mutta lokaalit pisteet paremmat kuin netissä
                    if (parhaatPisteet > 0)
                    {
                        ladatutParhaatPisteet.pisteData[index].parhaatPisteet = parhaatPisteet;
                    }
                    StartCoroutine(Paivita(ladatutParhaatPisteet));
                }
                else
                {
                    PlayerPrefs.SetInt(Pelikohtaiset.pelinNimi + "ParhaatPisteet" + ryhmaId, ladatutParhaatPisteet.pisteData[index].parhaatPisteet);

                }
            }
            else
            {
                // serveriltä ei löydy vielä pisteitä tällä nimellä joten lisätään lähetettävän listaan uusi
                if (parhaatPisteet > 0)
                {
                    ladatutParhaatPisteet.pisteData.Add(paikallisetPisteet);
                }
                StartCoroutine(Paivita(ladatutParhaatPisteet));
            }

            ladatutParhaatPisteet.pisteData.Sort((a, b) => a.parhaatPisteet.CompareTo(b.parhaatPisteet));
            ladatutParhaatPisteet.pisteData.Reverse();



            int iterat = 5;
            if (ladatutParhaatPisteet.pisteData.Count < 5)
            {
                iterat = ladatutParhaatPisteet.pisteData.Count;
            }
            for (int i = 0; i < iterat; i++)
            {
                if (ladatutParhaatPisteet != null)
                {
                    if (ladatutParhaatPisteet.pisteData[i] != null)
                    {
                        string teksti = ladatutParhaatPisteet.pisteData[i].lempiNimi + "   " + ladatutParhaatPisteet.pisteData[i].parhaatPisteet.ToString();
                        if (teksti != null)
                        {
                            parhaatTekstit[i].text = teksti;
                        }
                    }


                }

            }
        }


    }



    IEnumerator Paivita(ParhaatPisteetMalli paivitetytPisteet)
    {

        var paivitys = new UnityWebRequest(url + "/paivitapisteet", "POST");

        string json = JsonUtility.ToJson(paivitetytPisteet);
        byte[] groupIdByte = new System.Text.UTF8Encoding().GetBytes(json);
        paivitys.uploadHandler = (UploadHandler)new UploadHandlerRaw(groupIdByte);

        paivitys.SetRequestHeader("Content-Type", "application/json");

        paivitys.disposeUploadHandlerOnDispose = true;
        paivitys.disposeDownloadHandlerOnDispose = true;

        yield return paivitys.SendWebRequest();

        if (paivitys.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + paivitys.error);
        }
        else
        {
            Debug.Log("Connected to Highscore system");


        }
        paivitys.Dispose();

    }

    private void OnDestroy()  // Important for event cleanup
    {
        var tehtavatKartoitus = FindObjectOfType<TehtavatKartoitus>();
        if (tehtavatKartoitus != null)
        {
            tehtavatKartoitus.KartoitusValmis -= OnKartoitusValmis; // Unsubscribe in OnDestroy
        }
    }

}


