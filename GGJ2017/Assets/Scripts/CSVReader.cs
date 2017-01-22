using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct LocalisationData {
    public string ID;
    public string en;
    public string fr;
    public string de;
    public string es;
    public string it;
    public string jp;
    public string ch;
    public string bp;
    public string ko;
    public string se;

    public LocalisationData ( string _ID, string _en, string _fr, string _de, string _es ) {
        ID = _ID;
        en = _en;
        fr = _fr;
        de = _de;
        es = _es;
        it = _en;
        jp = _en;
        ch = _en;
        bp = _en;
        ko = _en;
        se = _en;
    }

    public LocalisationData (string _ID, string _en, string _fr, string _de, string _es, string _it, string _jp, string _ch, string _bp, string _ko, string _se ) {
        ID = _ID;
        en = _en;
        fr = _fr;
        de = _de;
        es = _es;
        it = _it;
        jp = _jp;
        ch = _ch;
        bp = _bp;
        ko = _ko;
        se = _se;
    }
}
[System.Serializable]
public struct LocalisedData {
    public string ID;
    public string localisedString;

    public LocalisedData(string _ID, string _localisedString ) {
        ID = _ID;
        localisedString = _localisedString;
    }
}
public enum Language {
    EN,
    FR,
    DE,
    ES,
    IT,
    JP,
    CH,
    BP,
    KO,
    SE
}

[System.Serializable]
public struct QuestionDataStrings
{
    public string ID;
    public string Question;
    public string Answer1;
	public string Answer2;
	public string Answer3;
    public string Referral;

	public QuestionDataStrings(string _ID, string _Question, string _Answer1, string _Answer2, string _Answer3, string _Referral){
        ID = _ID;
        Question = _Question;
        Answer1 = _Answer1;
        Answer2 = _Answer2;
        Answer3 = _Answer3;
        Referral = _Referral;
    }
}

[System.Serializable]
public struct QuestionData
{
    public int ID;
    public string Question;
    public string[] Answers;
    public int[] Referral;

    public QuestionData(int _ID, string _Question, string[] _Answers, int[] _Referral){
        ID = _ID;
        Question = _Question;
        Answers = _Answers;
        Referral = _Referral;
    }
}

public class CSVReader : MonoBehaviour {
public static CSVReader Instance;
    public TextAsset                csvFile             = null;
    public List<LocalisationData>   localisationData    = new List<LocalisationData>();
    public static LocalisedData[]   localisedData;
    public static bool              isReady             = false;
    public Language                 currentLanguage     = Language.EN;
    public bool                     overrideLanguage    = false;
    private int                     argCount            = 0;
    private int                     lineIndex           = 0;
    private string[]                lines               = null;

    public static string GetWord ( string ID ) {
        Language language = Language.EN;
        if ( !isReady ) {
            Debug.Log( "Loading " + Application.systemLanguage );
            switch ( Application.systemLanguage ) {
                case SystemLanguage.English:
                    language = Language.EN;
                    break;
                case SystemLanguage.French:
                    language = Language.FR;
                    break;
                case SystemLanguage.German:
                    language = Language.DE;
                    break;
                case SystemLanguage.Italian:
                    language = Language.IT;
                    break;
                case SystemLanguage.Spanish:
                    language = Language.ES;
                    break;
                case SystemLanguage.Japanese:
                    language = Language.JP;
                    break;
                case SystemLanguage.Chinese:
                    language = Language.CH;
                    break;
                case SystemLanguage.Korean:
                    language = Language.KO;
                    break;
                case SystemLanguage.Portuguese://??????????????????????
                    language = Language.BP;
                    break;
                case SystemLanguage.Swedish://??????????????????????
                    language = Language.SE;
                    break;
            }
            StaticLoadStrings( );
        }
        for ( int i = 0; i < localisedData.Length; i++ ) {
            if ( localisedData[i].ID == ID ) {
                return localisedData[i].localisedString;
            }
        }
        
        return ID;
    }
public List<QuestionDataStrings> qst = new List<QuestionDataStrings>();
    public List<QuestionData> qstData;
    // Use this for initialization
    void Awake () {
        Instance = this;
        if ( overrideLanguage ) {
            Load();
        }
        qst = StaticLoadStrings();
        qstData = ParseData(qst);
    }

	public List<QuestionData> GetData(){

        qst = StaticLoadStrings();
        return ParseData(qst);
	} 

    public static List<QuestionDataStrings> StaticLoadStrings ( ) {
        TextAsset file = Resources.Load<TextAsset>( "CaesarTalk" );
        string[] lines = file.text.Split( "\n"[0] );
        string[] firstLine = lines[0].Split(',');
        int argCount = 6;
        int lineIndex = 1;

        List < QuestionDataStrings > questionData = new List<QuestionDataStrings>();

        while ( lineIndex < lines.Length ) {
            List<string> vars = new List<string>();
            string currentLine = lines[lineIndex];
            int loopCount = 0;
            while ( vars.Count < argCount && loopCount < 1000 ) {
                loopCount++;
                if ( currentLine.Contains( "\"" ) ) {

                        Debug.Log(currentLine );
                    //The line contains either a comma or a new line character
                    int quoteIndex = currentLine.IndexOf("\"");

                    //Get all complete variables before the opening quote
                    string[] preQuote = currentLine.Substring(0, quoteIndex).Split(',');
                    for ( int i = 0; i < preQuote.Length - 1; i++ ) {
                        vars.Add( preQuote[i] );
                    }

                    //Add the next line and search for the closing quote
                    bool hasQuote=currentLine.Substring( quoteIndex+1 ).Contains("\"");
                    currentLine = currentLine.Substring( quoteIndex + 1 );
                    while ( !hasQuote ) {
                        currentLine += "\n" + lines[++lineIndex];
                        hasQuote = currentLine.Contains( "\"" );

                    }
                    int secondQuote = currentLine.Substring(1).IndexOf('"');
                    string finalVar = "\"" + currentLine.Substring( 0, secondQuote + 2 );
                    finalVar = finalVar.Remove( 0, 1 );
                    finalVar = finalVar.Remove( finalVar.Length-1, 1 );
                    vars.Add( finalVar );

                    //Set up the next line, minus the end of the previous variable
                    if ( lineIndex < lines.Length - 1 ) {
                        if ( secondQuote + 3 < currentLine.Length ) {
                            currentLine = currentLine.Substring( secondQuote + 3 );
                        }
                    } else{
                            currentLine = currentLine.Substring( secondQuote + 3 );

					}

                } else {
                    string[] variables = currentLine.Split(',');
                    vars.AddRange( variables );
                }
            }
            if ( loopCount < 1000 ) {
                if ( vars.Count > 5 ) {
                    questionData.Add(new QuestionDataStrings(vars[0], vars[1], vars[2], vars[3], vars[4], vars[5]));
					Instance.qst.Add(new QuestionDataStrings(vars[0], vars[1], vars[2], vars[3], vars[4], vars[5]));
                }
                lineIndex++;
            } else {
                Debug.LogError( "Inifinite loop, trying to end gracefully." );
            }
        }
		
        return questionData;
    }

	public List<QuestionData> ParseData(List<QuestionDataStrings> strings){
        List<QuestionData> data = new List<QuestionData>();
List<string> answers = new List<string>();

            for (int i = 0; i < strings.Count; i++)
            {
            answers.Clear();
			if(strings[i].Answer1.Length>0){
                answers.Add(strings[i].Answer1);
            }
			if(strings[i].Answer2.Length>0){
                answers.Add(strings[i].Answer2);
            }
			if(strings[i].Answer3.Length>0){
                answers.Add(strings[i].Answer3);
            }
            string[] referral = strings[i].Referral.Split(',');
            int[] refInt = new int[referral.Length];
                for (int j = 0; j < referral.Length; j++)
                {
                    refInt[j] = int.Parse(referral[j]);
                }

                data.Add(new QuestionData(int.Parse(strings[i].ID),
                strings[i].Question,
				answers.ToArray(),
                refInt));
            }
        return data;
    }

    public void Load ( ) {
        Loadfile();
        LoadLanguage();
    }

    public void Loadfile ( ) {
        if(csvFile == null ) {
            Debug.LogError( "File is null!" );
            csvFile = Resources.Load<TextAsset>( "Localisation" );
        }
        lines = csvFile.text.Split( "\n"[0] );
        string[] firstLine = lines[0].Split(',');
        argCount = firstLine.Length;
        lineIndex = 1;
        while(lineIndex < lines.Length ) {
            ParseNextLine();
        }
    }

    private void LoadLanguage ( ) {
        localisedData = new LocalisedData[localisationData.Count];
        for (int i = 0; i < localisationData.Count; i++ ) {
            switch ( currentLanguage ) {
                case Language.EN:
                    localisedData[i] = ( new LocalisedData( localisationData[i].ID, localisationData[i].en ) );
                    break;
                case Language.FR:
                    localisedData[i] = ( new LocalisedData( localisationData[i].ID, localisationData[i].fr ) );
                    break;
                case Language.DE:
                    localisedData[i] = ( new LocalisedData( localisationData[i].ID, localisationData[i].de ) );
                    break;
                case Language.ES:
                    localisedData[i] = ( new LocalisedData( localisationData[i].ID, localisationData[i].es ) );
                    break;
                case Language.IT:
                    localisedData[i] = ( new LocalisedData( localisationData[i].ID, localisationData[i].it ) );
                    break;
                case Language.JP:
                    localisedData[i] = ( new LocalisedData( localisationData[i].ID, localisationData[i].jp ) );
                    break;
                case Language.CH:
                    localisedData[i] = ( new LocalisedData( localisationData[i].ID, localisationData[i].ch ) );
                    break;
                case Language.BP:
                    localisedData[i] = ( new LocalisedData( localisationData[i].ID, localisationData[i].bp ) );
                    break;
                case Language.KO:
                    localisedData[i] = ( new LocalisedData( localisationData[i].ID, localisationData[i].ko ) );
                    break;
                case Language.SE:
                    localisedData[i] = ( new LocalisedData( localisationData[i].ID, localisationData[i].se ) );
                    break;
            }
        }
        isReady = true;
    }

    private void ParseNextLine ( ) {        
        List<string> vars = new List<string>();
        string currentLine = lines[lineIndex];
        int loopCount = 0;
        while ( vars.Count < argCount && loopCount < 1000 ) {
            loopCount++;
            if ( currentLine.Contains( "\"" ) ) {
                //The line contains either a comma or a new line character
                int quoteIndex = currentLine.IndexOf("\"");

                //Get all complete variables before the opening quote
                string[] preQuote = currentLine.Substring(0, quoteIndex).Split(',');
                for ( int i = 0; i < preQuote.Length - 1; i++ ) {
                    vars.Add( preQuote[i] );
                }

                //Add the next line and search for the closing quote
                bool hasQuote=currentLine.Substring( quoteIndex+1 ).Contains("\"");
                currentLine = currentLine.Substring( quoteIndex + 1 );
                while ( !hasQuote ) {
                    currentLine += "\n" + lines[++lineIndex];
                    hasQuote = currentLine.Contains( "\"" );
                }
                int secondQuote = currentLine.Substring(1).IndexOf('"');
                string finalVar = "\"" + currentLine.Substring( 0, secondQuote + 2 );
                finalVar = finalVar.Remove( 0, 1 );
                finalVar = finalVar.Remove( finalVar.Length - 1, 1 );
                vars.Add( finalVar );

                //Set up the next line, minus the end of the previous variable
                if ( lineIndex < lines.Length - 1 ) {                    
                    if ( secondQuote + 3 < currentLine.Length ) {
                        currentLine = currentLine.Substring( secondQuote + 3 );
                    }
                }

            } else {
                string[] variables = currentLine.Split(',');
                vars.AddRange( variables );
            }
        }
        if ( loopCount < 1000 ) {
            if ( vars.Count > 5 ) {
                localisationData.Add( new LocalisationData( vars[0], vars[1], vars[2], vars[3], vars[4], vars[5], vars[6], vars[7], vars[8], vars[9], vars[10] ) );
            } else {
                localisationData.Add( new LocalisationData( vars[0], vars[1], vars[2], vars[3], vars[4] ) );
            }
            lineIndex++;
        } else {
            Debug.LogError( "Inifinite loop, trying to end gracefully." );
        }
    }    
}