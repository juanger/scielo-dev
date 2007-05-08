class DataExtractor 
  def initialize( nameOpen, nameExit )
    @name_open = nameOpen
    @name_exit = nameExit
    @data = Array::new()
    @recordCollection = Array::new()
    @authorLine = ""
    @institutionLine = ""
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
      puts "#{@authorLine}"
      puts "#{@institutionLine}"
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
          value.split("\$\$x")
          countryName = value[1]
        end
        if value =~ /\$\$/
          data = value.split("\$\$")
          name = data[0]
        else
          name = data
        end

       #Country.find(:all).each {|country|
        #  if countryName == country.name
        #    countryID = country.id
        #  end
        #}
        @institutionLine = "INSERT INTO institutions VALUES ('"+name+"', '', '', , '',#{countryName}, '', '', '', '','','');"
      end
    }
  end
end

data1 = DataExtractor.new("../../../dataScieloIndex/dataDAT/clase30tmp.txt", "clase26abrExtract.txt")
data1.preProcessingFile( )
data1.collectionRecord( )
data1.preProcessingCollection( )
data1.prepareInsert( )

