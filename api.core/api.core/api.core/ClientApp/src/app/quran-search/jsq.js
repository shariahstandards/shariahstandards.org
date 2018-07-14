window.jquran = {};
document.write('<script type="text/javascript" src="'
    + 'jquranArabicv1.0.js' + '"></scr' + 'ipt>');
document.write('<script type="text/javascript" src="'
    + 'jquranEnglishYusufAli1.0.js' + '"></scr' + 'ipt>');


	
$(document).ready(function() {


    $('#quran').html(
        "<div id='jquran'>"
		+"<div id='searchResults'></div>"
		+"<div id='mainBody'>"
		+"<div id='textEntry'>"
		+"<div class=\"jquranLabel\"><label for=\"surahNumber\">Surah</label></div><div class=\"jquran.input\"><input id='surahNumber' name='surahNumber' type='text'/></div>"
        +"<div class=\"jquranLabel\"><label for=\"verseNumber\">Verse</label></div><div class=\"jquran.input\"><input id='verseNumber' name='verseNumber' type='text'/></div>"
        +"<div class=\"jquranLabel\"><label for=\"searchText\">Search</label></div><div class=\"jquran.input\"><input id='searchText' name='searchText' type='text'/></div>"
		+"<div id='searchButtonDiv'><input id='searchButton' type='button' value='start'/></div>"
		+"</div>"//end textEntry div		
		+"<div id='verse'>"
		+"<div id='arabicQuranOutput' "        
        +">&nbsp;</div>"
		+"<div id='englishQuranOutput' "
        +">&nbsp;</div>"
		+"</div>"//end verse div
		+"</div>"//end mainBody div
		+"</div>"//end jquran div
		);
	
	$('#searchText')[0].VKI_kt="Arabic";
	VKI_attach($('#searchText')[0]);	
		
		
    $('#surahNumber').keyup(function(event) {
        var number = Number($('#surahNumber').val());
        if (number >= 1 && number <= 114) {

            $('#verseNumber').val('1');
            showArabicVerse(number, 0);
        }
        else {
            $('#surahNumber').val('');
            $('#quranOutput').html('');
        }
		//alert(event.keyCode);
    });
	
	function VerseNumberChanged(event){
		if(event.keyCode==9 || event.keyCode==16 )//tabbed into this window
		{
			return;
		}
		//alert(event.keyCode);
		if (!isNaN($('#surahNumber').val())) {
            var surahNumber = Number($('#surahNumber').val());
            if (surahNumber >= 1 && surahNumber <= 114) {

                var displayVerseNumber = Number($('#verseNumber').val());
				if(event.keyCode==38)//up arrow
				{
					displayVerseNumber--;					
				}
				else if(event.keyCode==40)//down arrow
				{
					displayVerseNumber++;
				}				
				if(displayVerseNumber>jquran.arabicSurahs[surahNumber].length)
				{
					displayVerseNumber=jquran.arabicSurahs[surahNumber].length;
				}
				if(displayVerseNumber==0)
				{
					//displayVerseNumber=1;
				}
				
                if (displayVerseNumber > 0 && displayVerseNumber <= jquran.arabicSurahs[surahNumber].length) {                    
					$('#surahNumber').val(surahNumber);
					$('#verseNumber').val(displayVerseNumber);
					showArabicVerse(surahNumber, displayVerseNumber-1);				
                }
                else {
                    $('#verseNumber').val('');
                    $('#quranOutput').html('');
                }
            }
        }	
	}
	
     $('#verseNumber').keyup(function(event) {        
		 VerseNumberChanged(event);
     });
	
	$('#verseNumber').keydown(function(event) {		
		//VerseNumberChanged(event);
    });
	
     // $('#verseNumber').keypress(function(event) {		
		 // VerseNumberChanged(event);
     // });

    //    jquran.arabicQuranXMLPath = 'xml/surah{0}.xml';
        //jquran.englishQuranXMLPath = 'xml/english/englishsurah{0}.xml';
        //jquran.englishSurahs = [];
        //jquran.englishSurahsLoaded = 0;
    //    jquran.arabicSurahs = [];
    //    jquran.arabicSurahsLoaded = 0;
        //$('#quran').html("loading...please wait");
    
		function format(str) {
            for (i = 1; i < arguments.length; i++) {
                str = str.replace('{' + (i - 1) + '}', arguments[i]);
            }
            return str;
        }

		
		function buildArabicIndex(){
			window.jquran.arabicIndex=[];
			window.jquran.arabicWords=[]; 
			var wordCount=0;
			for(var s=1;s<=114;s++){
				for(var v=0;v<window.jquran.arabicSurahs[s].length;v++){
					for(var w=0;w<window.jquran.arabicSurahs[s][v].length;w++){
						var indexKey = window.jquran.arabicSurahs[s][v][w];
						if(window.jquran.arabicIndex[indexKey]==null){
							window.jquran.arabicIndex[indexKey]=[];							
							window.jquran.arabicWords[wordCount]=indexKey;
							wordCount++;
						}
						var ref={};
						ref.Surah=s;
						ref.Verse=v;
						ref.Word=w;	
						window.jquran.arabicIndex[indexKey].push(ref);
					}
				}
			}
		}
		
        function loadSurah(surahNumber) {
            $.ajax({
                type: "GET",
                url: format(jquran.englishQuranXMLPath, surahNumber),
                dataType: "xml",
                success: function(xml) {
                    $('#quran').html("loading...surah " + surahNumber);
                    window.jquran.englishSurahs[surahNumber] = [];
                    $(xml).find('Verse').each(function(verseNumber) {
                        window.jquran.englishSurahs[surahNumber][verseNumber] = $(this).text();
                    });
                    jquran.englishSurahsLoaded += 1;
                    if (jquran.englishSurahsLoaded == 114) {
                        $('#jsonBlock').html(JSON.stringify(window.jquran.englishSurahs));
                    }
                },
                error: function(errorMessage) {
                    // alert(errorMessage);
                }
            });
    //        $.ajax({
    //            type: "GET",
    //            url: format(jquran.arabicQuranXMLPath, surahNumber),
    //            dataType: "xml",
    //            success: function(xml) {
    //                $('#quran').html("loading...surah " + surahNumber);
    //                window.jquran.arabicSurahs[surahNumber] = [];
    //                $(xml).find('Verse').each(function(verseNumber) {
    //                    window.jquran.arabicSurahs[surahNumber][verseNumber] = [];
    //                    $(this).find('word').each(function(wordNumber) {
    //                        window.jquran.arabicSurahs[surahNumber][verseNumber][wordNumber] = $(this).text();
    //                    });
    //                });
    //                jquran.englishSurahsLoaded += 1;
    //                if (jquran.englishSurahsLoaded == 114) {
    //                    
    //                }
    //            },
    //            error: function(errorMessage) {
    //                // alert(errorMessage);
    //            }
    //        });
        }

		window.gotoReference = function(surah,verse,word){
				$('#surahNumber').val(surah);
				$('#verseNumber').val(verse+1);				
				showArabicVerse(surah,verse,word);								
		}
		function GetArabicWordSearchResult(word){
			var results = [];
			results.push("<div class='jquranSearchResult'>"+word+"<span style='font-size:10pt;direction:ltr'> \u200E &nbsp;&nbsp;("+window.jquran.arabicIndex[word].length+")</span></div>");
			results.push("<ul style='display:none'>");
			for(var i=0;i<window.jquran.arabicIndex[word].length;i++){
					var ref = window.jquran.arabicIndex[word][i];
					results.push("<li><a href='#' onclick='javascript:gotoReference("+ref.Surah+","+ref.Verse+","+ref.Word+")'>"+ref.Surah+":"+(ref.Verse+1)+"</a></li>");				
			}
			results.push("</ul>");
			return results.join('\n');
		}
		function stripArabicVowels(word)
		{
			var output=word;
			var chars=['\u064e','\u064f','\u0650','\u0652'];
			for(var c=0;c<chars.length;c++){
				output=(output.split(chars[c])).join('');				
			}
			return output;
		}
		function search(){
		
			$('#searchResults').html("");
			
			var searchText  = $('#searchText').val();			
			if(searchText=="") return;
			var resultsFound=false;
			var exactMatch = window.jquran.arabicIndex[searchText];
			if(exactMatch!=null){
				$('#searchResults').append("<div>Exact Matches</div>");
				$('#searchResults').append(GetArabicWordSearchResult(searchText));
				resultsFound=true;
			}
			
			var partialExactMatches = [];
			var partialExactMatchesCount = 0;
			for(var w=0;w<window.jquran.arabicWords.length;w++){
				if(window.jquran.arabicWords[w].toString().indexOf(searchText)>=0){
					partialExactMatches[partialExactMatchesCount]=window.jquran.arabicWords[w];
					partialExactMatchesCount++;
				}
			}				
			if(partialExactMatchesCount>0){
				partialExactMatches.sort();
				$('#searchResults').append("<div>"+partialExactMatchesCount+" Partial Exact Matches</div>");
				for(var i=0;i<partialExactMatchesCount;i++){
					$('#searchResults').append(GetArabicWordSearchResult(partialExactMatches[i]));
					resultsFound=true;
				}
			}
			var consonantMatches = [];
			var consonantMatchesCount = 0;
			var consonantOnlySearchWord=stripArabicVowels(searchText);
			for(var w=0;w<window.jquran.arabicWords.length;w++){
				var testWord=stripArabicVowels(window.jquran.arabicWords[w]);
				if(testWord.indexOf(consonantOnlySearchWord)>=0){
					consonantMatches[consonantMatchesCount]=window.jquran.arabicWords[w];
					consonantMatchesCount++;
				}
			}				
			if(consonantMatchesCount>0){
				consonantMatches.sort();
				$('#searchResults').append("<div>"+consonantMatchesCount+" Consonant Matches</div>");
				for(var i=0;i<consonantMatchesCount;i++){
					$('#searchResults').append(GetArabicWordSearchResult(consonantMatches[i]));
					resultsFound=true;
				}
			}
			
			var englishMatches=[];
			var englishMatchesCount=0;
			var testEnglishSearch =searchText.toLowerCase();
			for(var s=1;s<=114;s++){
				for(var v=0;v<jquran.englishSurahs[s].length;v++){
					var verse = jquran.englishSurahs[s][v].toLowerCase();
					
					if(verse.indexOf(testEnglishSearch)>=0){
						englishMatches.push("<li><a href='#' onclick='javascript:gotoReference("+s+","+v+")'>"+s+":"+(v+1)+"</a></li>");
						englishMatchesCount++;
						resultsFound=true;
					}
				}
			}				
			if(englishMatchesCount>0){				
				$('#searchResults').append("<div>"+englishMatchesCount+" English Matches</div>");
				$('#searchResults').append("<ul>");
				$('#searchResults').append(englishMatches.join('\n'));
				$('#searchResults').append("</ul>");				
			}
			
			if(!resultsFound){
				$('#searchResults').append("<div> No Matches</div>");			
			}
			
			$('.jquranSearchResult').each(function(){
				$(this).click(function(){
					$(this).next().toggle();
				});
			});
		}
		$('#searchButton').click(search);
		for (var s = 1; s <= 114; s++) {
   //     loadSurah(s);
	}
	buildArabicIndex();
		// surah number is normal number verseNumber and highLightedWordNumber are zerobased indexes
    window.showArabicVerse=function(surahNumber, verseNumber, highLightedWordNumber) {
        var verse = window.jquran.arabicSurahs[surahNumber][verseNumber];
		var result=[];
		for(var w=0;w<verse.length;w++){
			if(highLightedWordNumber==w){
				result.push("<span class='highlight'>"+verse[w]+"</span>");				
			}
			else{
				result.push(verse[w]);
			}
		}
		
        $('#arabicQuranOutput').html(result.join(" "));
		$('#englishQuranOutput').html(window.jquran.englishSurahs[surahNumber][verseNumber]);
    }


});