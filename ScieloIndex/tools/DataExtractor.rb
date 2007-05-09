require "Country"
class DataExtractor_new 
  def initialize( nameOpen, nameExit )
    @name_open = nameOpen
    @name_exit = nameExit
    @data = Array::new()
    @recordCollection = Array::new()
    @authorLine = ""
    @institutionLine = ""
    @articleLine = ""
    @journalLine = ""
    @journal_issueLine = ""
  end # of unitialize
  
  def preProcessingFile( )
    open( @name_exit, 'w' ) do |f| 
      File.open( @name_open ).each { |line|
        f.puts line if line !~ / CAT| OWN| FMT| LDR| 035| 036/
      }
    end
  end
  
  def collectionRecord( )

    key = "000000001"
    cadena = ""
    File.open( @name_exit ).each{ |line|
      
      array = line.split(" ")
      keyline = array [0];
      
      if ( key == keyline )
        cadena += line
      else
        key = keyline
        @data.push(cadena)
        cadena = ""
      end
    }
    @data.push(cadena)
  end

  def preProcessingCollection( )
    index = 0
    @data.each{ |element|
      array = element.split(" ")
      key = array[0].concat(" ")
      elementTmp = element.gsub(key,"")
      elementTmp = elementTmp.gsub("   L $$","   $$")
      @data[index] = elementTmp
      index = index+1
    }
    
    @data.each{ |record|
      elements = record.split("\n")
      pairValues = Array::new( )
      elements.each{ |pair|
        pairs = pair.split("   ")
        pairValues.push(pairs)
      }
      @recordCollection.push(pairValues)
    }
  end
  
  def prepareInsert( )
    @recordCollection.each{ |record|
      getDataAuthor( record )
      getDataInstitution( record )
      getDataArticle( record )
      getDataJournal( record )
      getDataJournalIssue( record )
      puts "#{@authorLine}"
      puts "#{@institutionLine}"
      puts "#{@articleLine}"
      puts "#{@journalLine}"
      puts "#{@journal_issueLine}"
    }
  end
  
  def getDataAuthor( record )
    record.each { |element|
      key = element[0]
      value = element[1]
      if key == "100" && value =~ /\$\$a/
        value = value.gsub("\$\$a","")
        data = value.split(", ")
        @authorLine = "INSERT INTO authors VALUES ('"+data[1]+"', '', '"+data[0]+"', '');"
      end
    }
  end
  
  def getDataInstitution( record )
    name = ""
    countryName = ""
    countryID = -1
    record.each { |element|
      key = element[0]
      value = element[1]
      if key == "100" && value =~ /\$\$u/
        value = value.gsub("\$\$u","")
        if value =~ /\$\$x/
          content = value.split("$$x")
          countryName = content[1]
        end
        if value =~ /\$\$/
          data = value.split("\$\$")
          name = data[0]
        else
          name = data
        end
        
        Country.find(:all).each {|country|
          if countryName == country.name
            countryID = country.id
          end
        }
        
        @institutionLine = "INSERT INTO institutions VALUES ('"+name+"', '', '', , '',#{countryID}, '', '', '', '','','');"
      end
    }
  end

  def getDataArticle( record)
    title = ""
    page_range = ""
    record.each { |element|
      key = element[0]
      value = element[1]
      if key == "300" && value =~ /\$\$e/
        content = value.split("\$\$e")
        page_range = content[1]
      end
      if key == "245" && value =~ /\$\$a/
        value = value.gsub("\$\$a","")
        title = value
      end
      @articleLine = "INSERT INTO articles VALUES ('"+title+"', '', '', '"+page_range+"', '', '', '');"
    }
  end
  
  def getDataJournal( record )
    issn = ""
    record.each {|element|
      key = element[0]
      value = element[1]
      if key == "022" && value =~ /\$\$a/
        value = value.gsub("\$\$a","")
        issn = value
        @journalLine = "INSERT INTO journals VALUES ('','"+issn+"', '');"
      end
    }
  end

  def getDataJournalIssue( record )
    number = ""
    volume = ""
    content = ""
    year = -1
    record.each {|element|
      key = element[0]
      value = element[1]
      if key == "300" 
        if value =~ /\$\$b/
          content = value.split("\$\$b")
          content2 = content[1].split("\$\$")
          number = content2[0]
        end
        if value =~ /\$\$a/
          volume = content[0].gsub("\$\$a","")
        end
      end
      if key == "260" && value =~ /\$\$b/
        value = value.gsub("\$\$b","")
        year = value
      end
      @journal_issueLine = "INSERT INTO journal_issues VALUES ('','"+number+"', '"+volume+"', #{year});"
    }
  end

  def getDataCollection( record )
    title = ""
    record.each {|element|
      key = element[0]
      value = element[1]
      if key == "222" && value =~ /\$\$b/
        content = value.split("\$\$b")
        content2 = content[1].split("\$\$")
        number = content2[0]
        @journal_issueLine = "INSERT INTO journal_issues VALUES ('','"+number+"', '"+volume+"', #{year});"
      end
      }
  end
  
end

data1 = DataExtractor_new.new("../../../dataScieloIndex/dataDAT/clase30tmp.txt", "clase26abrExtract.txt")
data1.preProcessingFile( )
data1.collectionRecord( )
data1.preProcessingCollection( )
data1.prepareInsert( )

