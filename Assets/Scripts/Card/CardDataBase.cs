using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class CardDataBase
{
   /// <summary>
   /// Diccionario donde a cada facción se le hace corresponder su mazo
   /// </summary>
   public static Dictionary<Global.Factions, Deck> Decks = new Dictionary<Global.Factions, Deck>();
   public static Deck Gryffindor; //mazo de la facción Gryffindor
   public static Deck Slytherin; //mazo de la facción Slytherin
   public static Deck Ravenclaw;
   public static Deck Hufflepuff;
   public static List<UnitCard> Neutral = new List<UnitCard>(); //lista de unidades neutrales
   public static List<SpecialCard> Specials = new List<SpecialCard>(); //lista de cartas especiales neutrales
   /// <summary>
   /// Diccionario donde a cada facción se le hace corresponder su líder
   /// </summary>
   public static Dictionary<Global.Factions, Leader> Leaders = new Dictionary<Global.Factions, Leader>();
   static CardDataBase()
   {
      //Leaders
      Leaders.Add(Global.Factions.Gryffindor, new Leader("Harry Potter", Global.Factions.Gryffindor, new List<Skill>() { new Skill("LeaderGryffindor", null, null) },
     "El niño que vivió. El elegido para derrotar al señor tenebroso", Resources.Load<Sprite>("HarryPotter")));
      Leaders.Add(Global.Factions.Slytherin, new Leader("Lord Voldemort", Global.Factions.Slytherin, new List<Skill>() { new Skill("LeaderSlytherin", null, null) },
      "Un ser cuya ambición y sed de poder trascienden los límites de la cordura. Su nombre, susurrado con temor y evitado como una maldición.",
      Resources.Load<Sprite>("LordVoldemort")));

      //Specials

      //Weathers
      Specials.Add(new Weather("Escarcha Heladora", new List<Skill>() { new Skill("WeatherMelee", null, null) },
      "En los confines más gélidos de la Escuela de Magia y Hechicería de Hogwarts, un fenómeno mágico sin igual se desencadena: la Escarcha Heladora. Evoca la esencia misma del invierno, transformando el majestuoso Castillo de Hogwarts en una fortaleza de hielo.",
      Resources.Load<Sprite>("EscarchaHeladora"), Resources.Load<Sprite>("Frost")));
      Specials.Add(new Weather("Niebla Profunda", new List<Skill>() { new Skill("WeatherRanged", null, null) },
      "En los rincones más oscuros y misteriosos del Bosque Prohibido de Hogwarts, una niebla densa y profunda se alza como un velo entre los árboles retorcidos. Envuelve el bosque en un abrazo etéreo, ocultando secretos ancestrales y criaturas inimaginables.",
      Resources.Load<Sprite>("NieblaProfunda"), Resources.Load<Sprite>("Fog")));
      Specials.Add(new Weather("Diluvio Quidditch", new List<Skill>() { new Skill("WeatherSiege", null, null) },
      "En el corazón del Estadio de Quidditch, donde los sueños de los magos y brujas se elevan junto con las escobas, se desata un diluvio mágico sin igual: el Diluvio Quidditch. Sumerge el campo de juego en una tormenta desenfrenada, desafiando a los jugadores a luchar contra las ráfagas de viento y las gotas de lluvia que caen como saetas.",
      Resources.Load<Sprite>("DiluvioQuidditch"), Resources.Load<Sprite>("Rain")));

      //Clear
      Specials.Add(new Clear("Sol de Hogwarts", new List<Skill>() { new Skill("ClearWeather", null, null) },
      "En el corazón mismo de la Escuela de Magia y Hechicería de Hogwarts, un día de ensueño se despliega sobre las torres y los patios. Captura este momento mágico, cuando los rayos dorados del sol acarician las piedras ancestrales y las ventanas de vitrales.",
      Resources.Load<Sprite>("SolDeHogwarts")));
      Specials.Add(new Clear("Gira Tiempo", new List<Skill>() { new Skill("ClearWeather", null, null) },
      "En el rincón más recóndito de la Biblioteca Prohibida de Hogwarts, resplandece una reliquia mágica de poder insondable cuyo cristal centella con destellos dorados. Quien lo posea, posee el poder de alterar el pasado y el futuro, de desafiar las leyes del universo.",
      Resources.Load<Sprite>("GiraTiempo")));

      //Boost
      Specials.Add(new Boost("Expecto Patronum", new List<Skill>() { new Skill("Boost", null, null) },
      "Invocar un Patronus es un acto de gran poder. El Patronus repele a los Dementores y las fuerzas oscuras, brindando protección y fortaleza.",
      Resources.Load<Sprite>("ExpectoPatronum")));
      Specials.Add(new Boost("Dobby", new List<Skill>() { new Skill("Boost", null, null) },
      "El elfo doméstico liberado por Harry. Aunque inicialmente sirve a la familia Malfoy, luego se convierte en un aliado leal de Harry y lucha por la libertad de los elfos domésticos",
      Resources.Load<Sprite>("Dobby")));


      //Decks
      Gryffindor = new Deck(Leaders[Global.Factions.Gryffindor]);
      Slytherin = new Deck(Leaders[Global.Factions.Slytherin]);

      //Decoy
      Neutral.Add(new DecoyUnit("Hagrid",
      "Es más que un guardián de llaves y terrenos. Es un amigo leal, un mentor apasionado y un defensor incansable de la magia y la naturaleza. Cuida y protege a todas las criaturas, sin importar su tamaño o peligro.",
      Resources.Load<Sprite>("Hagrid")));


      //Neutral
      Neutral.Add(new HeroUnit("Albus Dumbledore", Global.Factions.Neutral, new List<Skill>() { new Skill("DrawCard", null, null) }, "El Gran Hechicero, director de Hogwarts, el mago más grande de su tiempo. Su sombra se alza como un faro, guiando a las generaciones futuras hacia la luz.",
      12, new List<Global.AttackModes>() { Global.AttackModes.Ranged }, Resources.Load<Sprite>("AlbusDumbledore")));
      Neutral.Add(new Unit("Wingardium Leviosa", Global.Factions.Neutral, new List<Skill>() { new Skill("PutWeather", null, null) }, "Es un encantamiento usado para hacer que los objetos leviten o vuelen. Se enseña en el primer año en la clase de Encantamientos en el Colegio Hogwarts de Magia y Hechicería.",
      1, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("WingardiumLeviosa")));
      Neutral.Add(new Unit("Expelliarmus", Global.Factions.Neutral, new List<Skill>() { new Skill("PowerfulCard", null, null) }, "Es un hechizo de desarme ampliamente utilizado en el mundo mágico, el objetivo es despojado de su arma o varita. El objeto en cuestión sale volando de las manos del oponente y cae al suelo.",
      0, new List<Global.AttackModes>() { Global.AttackModes.Siege }, Resources.Load<Sprite>("Expelliarmus")));
      Neutral.Add(new Unit("Argus Filch", Global.Factions.Neutral, new List<Skill>() { new Skill("LessPowerCard", null, null) }, "El cuidador de Hogwarts, inflexible cuando se trata de las normas escolares. Su gato, Mrs. Norris, lo acompaña en sus rondas nocturnas para atrapar a los infractores.",
      0, new List<Global.AttackModes>() { Global.AttackModes.Ranged }, Resources.Load<Sprite>("ArgusFilch")));
      Neutral.Add(new Unit("Peter Pettigrew", Global.Factions.Neutral, new List<Skill>() { }, "aunque cobarde y traidor, es un recordatorio de que incluso los personajes más oscuros tienen profundidad y complejidad",
      3, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("PeterPettigrew")));
      Neutral.Add(new Unit("Fawkes", Global.Factions.Neutral, new List<Skill>() { new Skill("ClearRow", null, null) }, "Se cree que Fawkes ha existido durante siglos. Sus lágrimas tienen propiedades curativas y su canto puede inspirar coraje o miedo. Fue el fiel compañero y defensor del director de Hogwarts, Albus Dumbledore.",
      3, new List<Global.AttackModes>() { Global.AttackModes.Siege }, Resources.Load<Sprite>("Fawkes")));
      Neutral.Add(new Unit("Avada Kedavra", Global.Factions.Neutral, new List<Skill>() { }, "El hechizo causa una muerte instantánea y sin dolor, sin causar ninguna herida en el cuerpo y sin dejar rastro de violencia.",
      10, new List<Global.AttackModes>() { Global.AttackModes.Ranged }, Resources.Load<Sprite>("AvadaKedavra")));

      //Gryffindor
      Gryffindor.AddCard(new Unit("Arthur Weasley", Global.Factions.Gryffindor, new List<Skill>() { }, "Patriarca de la Familia Weasley, conocido por su amor por los objetos muggles, su curiosidad científica y su papel en el Ministerio de Magia",
      5, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("ArthurWeasley")));
      Gryffindor.AddCard(new Unit("Colin Creevey", Global.Factions.Gryffindor, new List<Skill>() { }, "Durante su primer año en Hogwarts, hizo muchas fotos de Harry y otros estudiantes. A lo largo de su tiempo en la escuela, su admiración por Harry nunca disminuyó.",
      3, new List<Global.AttackModes>() { Global.AttackModes.Ranged }, Resources.Load<Sprite>("ColinCreevey")));
      Gryffindor.AddCard(new Unit("Fred y George Weasley", Global.Factions.Gryffindor, new List<Skill>() { }, "Magos gemelos, ambos son expertos en bromas y fundaron la tienda de artículos de broma Sortilegios Weasley.",
      4, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("Fred&GeorgeWeasley")));
      Gryffindor.AddCard(new Unit("Ginny Weasley", Global.Factions.Gryffindor, new List<Skill>() { }, "es una bruja valiente, apasionada y leal, y su papel en la lucha contra las fuerzas oscuras es fundamental.",
      6, new List<Global.AttackModes>() { Global.AttackModes.Ranged }, Resources.Load<Sprite>("GinnyWeasley")));
      Gryffindor.AddCard(new Unit("Hedwing", Global.Factions.Gryffindor, new List<Skill>() { new Skill("PutBoost", null, null) }, "Hedwig es inteligente y leal. A menudo se posaba en el hombro de Harry y lo acompañaba en sus aventuras. Su afecto y lealtad hacia él eran evidentes",
      2, new List<Global.AttackModes>() { Global.AttackModes.Siege }, Resources.Load<Sprite>("Hedwing")));
      Gryffindor.AddCard(new HeroUnit("Hermione Granger", Global.Factions.Gryffindor, new List<Skill>() { new Skill("Average", null, null) }, "La bruja más inteligente de su generación, la que siempre tiene la respuesta a todo. Fundadora del Ejército de Dumbledore, una organización secreta para enseñar defensa contra las artes oscuras.",
      8, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("HermioneGranger")));
      Gryffindor.AddCard(new Unit("James y Lily Potter", Global.Factions.Gryffindor, new List<Skill>() { }, "Su amor y sacrificio dejaron una marca indeleble en la historia mágica y en el corazón de su hijo, Harry.",
      6, new List<Global.AttackModes>() { Global.AttackModes.Ranged }, Resources.Load<Sprite>("James&LilyPotter")));
      Gryffindor.AddCard(new Unit("Lavender Brown", Global.Factions.Gryffindor, new List<Skill>() { }, "Lavender era una chica algo tonta y sentimental, aunque también era valiente, tenía un gran interés en la Adivinación.",
      3, new List<Global.AttackModes>() { Global.AttackModes.Siege }, Resources.Load<Sprite>("LavenderBrown")));
      Gryffindor.AddCard(new Unit("Minerva McGonagall", Global.Factions.Gryffindor, new List<Skill>() { }, "trbajó en el Ministerio de Magia y luego regresó a Hogwarts, donde se convirtió en Jefa de la Casa Gryffindor, profesora de Transformaciones y subdirectora de Hogwarts. Lucha por lo justo y honesto.",
      11, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("MinervaMcGonagall")));
      Gryffindor.AddCard(new Unit("Neville Longbottom", Global.Factions.Gryffindor, new List<Skill>() { }, "Pasó de ser un chico tímido e introvertido a ser un valiente defensor de Hogwarts, responsable de la decapitación de Nagini.",
      6, new List<Global.AttackModes>() { Global.AttackModes.Ranged }, Resources.Load<Sprite>("NevilleLongbottom")));
      Gryffindor.AddCard(new Unit("Parvati Patil", Global.Factions.Gryffindor, new List<Skill>() { }, "Sus asignaturas preferidas son Adivinación y Cuidado de Criaturas Mágicas, Lavender Brown es su mejor amiga.",
      3, new List<Global.AttackModes>() { Global.AttackModes.Siege }, Resources.Load<Sprite>("ParvatiPatil")));
      Gryffindor.AddCard(new HeroUnit("Remus Lupin", Global.Factions.Gryffindor, new List<Skill>() { new Skill("PutWeather", null, null) }, "Mago consumado y hábil, con un amplio conocimiento de Artes Oscuras. También poseía la capacidad de impartir adecuadamente habilidades prácticas y teóricas de magia defensiva a otros. Remus era capaz de conjurar a un Patronus lobo corpóreo.",
      7, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("RemusLupin")));
      Gryffindor.AddCard(new HeroUnit("Sirius Black", Global.Factions.Gryffindor, new List<Skill>() { new Skill("PutBoost", null, null) }, "El fugitivo de Askaban, valiente y leal, como lo demuestra su participación en ambas guerras y la disposición a morir por sus seres queridos. Era un mago talentoso e inteligente cuyo animago era un gran perro negro.",
      9, new List<Global.AttackModes>() { Global.AttackModes.Ranged }, Resources.Load<Sprite>("SiriusBlack")));
      Gryffindor.AddCard(new Unit("Ron Weasley", Global.Factions.Gryffindor, new List<Skill>() { new Skill("MultiplyPower", null, null) }, "Demostró ser un poderoso mago por derecho propio, leal, valiente y a que menudo muestra un gran sentido del humor. Mejor amigo de Harry y Hermione.",
      5, new List<Global.AttackModes>() { Global.AttackModes.Siege }, Resources.Load<Sprite>("RonWeasley")));
      Gryffindor.AddCard(new Unit("William Weasley", Global.Factions.Gryffindor, new List<Skill>() { }, "Hijo mayor de los Wesley, demostró una valentía inquebrantable. Su espíritu indomable lo llevó a enfrentarse a Lord Voldemort y sus seguidores, arriesgando su vida por la seguridad de los demás.",
      4, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("WilliamWeasley")));

      //Slytherin
      Slytherin.AddCard(new Unit("Andromeda Tonks", Global.Factions.Slytherin, new List<Skill>() { }, "Bruja Black, valiente que desafió las normas y defendió el amor por encima de la pureza de sangre al casarse con un muggel.",
      4, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("AndromedaTonks")));
      Slytherin.AddCard(new Unit("Astoria Malfoy", Global.Factions.Slytherin, new List<Skill>() { }, "Se crió en los ideales de la supremacía de la sangre pura, se vio afectada por una maldición impuesta a su antepasado mucho antes de su tiempo, lo que hizo que su cuerpo se volviera extremadamente frágil.",
      3, new List<Global.AttackModes>() { Global.AttackModes.Ranged }, Resources.Load<Sprite>("AstoriaMalfoy")));
      Slytherin.AddCard(new HeroUnit("Bellatrix Lestrange", Global.Factions.Slytherin, new List<Skill>() { new Skill("PutWeather", null, null) }, "Mortífaga fanáticamente leal a Lord Voldemort. Era una de las pocas mujeres en el grupo, al igual que una de los más peligrosos y sádicos seguidores de Voldemort.",
      9, new List<Global.AttackModes>() { Global.AttackModes.Siege }, Resources.Load<Sprite>("BellatrixLestrange")));
      Slytherin.AddCard(new Unit("Dolores Umbridge", Global.Factions.Slytherin, new List<Skill>() { new Skill("DrawCard", null, null) }, "Nombrada Suma Inquisidora de Hogwarts es cruel y sádica, que disfruta infligiendo dolor a los demás. Cree que la fuerza es la única forma de mantener el orden y está obsesionada con las reglas y la disciplina.",
      8, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("DoloresUmbridge")));
      Slytherin.AddCard(new HeroUnit("Draco Malfoy", Global.Factions.Slytherin, new List<Skill>() { new Skill("PowerfulCard", null, null) }, "Cruel, hambriento de poder y, en última instancia, cobarde. Aparenta ser un bravucón intrépido, malcriado y grosero, y no muestra remordimiento por el mal que ha causado.",
      7, new List<Global.AttackModes>() { Global.AttackModes.Ranged }, Resources.Load<Sprite>("DracoMalfoy")));
      Slytherin.AddCard(new Unit("Gregory Goyle", Global.Factions.Slytherin, new List<Skill>() { }, "matón que utiliza su tamaño y fuerza para intimidar. Aunque lento en comprensión, es leal a Draco Malfoy.",
      4, new List<Global.AttackModes>() { Global.AttackModes.Siege }, Resources.Load<Sprite>("GregoryGoyle")));
      Slytherin.AddCard(new Unit("Horace Slughorn", Global.Factions.Slytherin, new List<Skill>() { }, " Maestro de Pociones y Jefe de la Casa Slytherin. Escogía a sus alumnos favoritos, a veces por la ambición o la inteligencia que demostraban, otras por su encanto o su talento.",
      5, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("HoraceSlughorn")));
      Slytherin.AddCard(new Unit("Lucius Malfoy", Global.Factions.Slytherin, new List<Skill>() { new Skill("PutBoost", null, null) }, " Patriarca Malfoy, mortífago, fiel a Lord Voldemort, y participó en la Primera Guerra Mágica. ",
      6, new List<Global.AttackModes>() { Global.AttackModes.Ranged }, Resources.Load<Sprite>("LuciusMalfoy")));
      Slytherin.AddCard(new Unit("Millicent Bulstrode", Global.Factions.Slytherin, new List<Skill>() { }, "Su pertenencia a Slytherin y su participación en la Brigada Inquisitorial sugieren que comparte las opiniones de pureza de sangre y es leal a su casa.",
      3, new List<Global.AttackModes>() { Global.AttackModes.Siege }, Resources.Load<Sprite>("MillicentBulstrode")));
      Slytherin.AddCard(new Unit("Nagini", Global.Factions.Slytherin, new List<Skill>() { new Skill("Average", null, null) }, "Su veneno fue utilizado por Voldemort como uno de los ingredientes para recuperar su fuerza después de su caída inicial.",
      2, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("Nagini")));
      Slytherin.AddCard(new Unit("Narcissa Malfoy", Global.Factions.Slytherin, new List<Skill>() { }, "Esposa del mortífago Lucius Malfoy y madre de Draco. Aunque nunca se declaró oficialmente como mortífaga, Narcissa creía en la importancia de la pureza de la sangre y estaba a favor de que su marido siguiera a Lord Voldemort.",
      3, new List<Global.AttackModes>() { Global.AttackModes.Ranged }, Resources.Load<Sprite>("NarcissaMalfoy")));
      Slytherin.AddCard(new Unit("Pansy Parkinson", Global.Factions.Slytherin, new List<Skill>() { }, "Líder del grupo de las chicas de Slytherin. Ella y Draco eran mezquinos y tenían una considerable influencia y poder dentro de su grupo de amigos, y se sirvieron de ese poder para intimidar a otros estudiantes.",
      4, new List<Global.AttackModes>() { Global.AttackModes.Siege }, Resources.Load<Sprite>("PansyParkinson")));
      Slytherin.AddCard(new Unit("Regulus Black", Global.Factions.Slytherin, new List<Skill>() { }, "Hermano menor de Sirus Black, creía en la sangre pura. Desempeñó un papel crucial en la búsqueda y recuperación del Guardapelo de Slytherin, uno de los Horrocruxes de Voldemort.",
      7, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("RegulusBlack")));
      Slytherin.AddCard(new HeroUnit("Severus Snape", Global.Factions.Slytherin, new List<Skill>() { }, "Snape era un hechicero formidable, mostrando una habilidad consumada en muchas ramas diferentes de magia, Maestro de Posiones y Defensa Contra las Artes Oscuras.",
      11, new List<Global.AttackModes>() { Global.AttackModes.Siege }, Resources.Load<Sprite>("SeverusSnape")));
      Slytherin.AddCard(new Unit("Vincent Crabbe", Global.Factions.Slytherin, new List<Skill>() { }, "Lacayo de Draco Malfoy, hijo de mortífago y futuro mago oscuro que conjura varios hechizos imperdonables a Hermione",
      4, new List<Global.AttackModes>() { Global.AttackModes.Melee }, Resources.Load<Sprite>("VincentCrabbe")));

      //añadiendo unidades neutrales
      foreach (var card in Neutral)
      {
         Gryffindor.AddCard(card);
         Slytherin.AddCard(card);
      }

      //añadiendo especiales neutrales
      foreach (var card in Specials)
      {
         Gryffindor.AddCard(card);
         Slytherin.AddCard(card);
      }

      Decks.Add(Global.Factions.Gryffindor, Gryffindor);
      Decks.Add(Global.Factions.Slytherin, Slytherin);
   }



}
