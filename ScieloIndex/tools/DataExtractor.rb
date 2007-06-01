require "Country"
require "Author"
require "Institution"
require "Journal"
require "JournalIssue"
require "Publisher"
require "Article"

class DataExtractor_new 
  def initialize( nameOpen, nameExit )
    @name_open = nameOpen
    @name_exit = nameExit
    @data = Array::new()
    @recordCollection = Array::new()
    @countryID
    @authorID 
    @institutionID
    @articleID
    @journalID
    @journalIssueID
    @titleJournal
    @journal_issueLine = ""
    @collectionLine = ""
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
    DataPublisher()
    @recordCollection.each{ |record|
      getDataAuthor( record )
      getDataInstitution( record )
      getDataCollection( record )
      getDataJournal( record )
      getDataJournalIssue( record )
      getDataArticle( record )
    }
    #Author.find(:all).each { |author|
    #  puts "Id: #{author.id} Fname: #{author.firstname}, Lname: #{author.lastname}"
    #}
    #Institution.find(:all).each { |institution|
    #  puts "Id: #{institution.id} name: #{institution.name}, country: #{institution.country_id}"
    #}
  end
  
  def DataPublisher
    Publisher.new(:name => "NO_SPECIFIED")
  end

  def getDataAuthor( record )
    record.each { |element|
      key = element[0]
      value = element[1]
      
      if key == "100" && value =~ /\$\$a/
        value = value.gsub("\$\$a","")
        data = value.split(", ")
        
        author = Author.new(:firstname => data[1], :lastname => data[0])
        if author.save
          @authorID = author.id
        else
          puts"ERROR::getDataAuthor:NoInsercion"
        end
      end
    }
  end
  
  def getDataInstitution( record )
    name = ""
    countryName = ""
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
            
            @countryID = country.id
            institution = Institution.new(:name => name, :country_id => @countryID);
            
            if(!Institution.exists?(:name => name, :country_id => @countryID))
              if(institution.save)
                @institutionID = institution.id
              else
                puts"ERROR::getDataInstitution:NoInsercion"
              end
            end
          end
        }
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
        @title = value
      end
      article = Article.new(:title => title, :page_range => page_range, :journal_issue_id => @journalIssueID)
      if(!Article.exists?(:title => title, :journal_issue_id => @journalIssueID ))
        if( @journalIssueID == nil )
          puts "ERROR::getDataArticle:JournalIssueID_NIL"
        else
          if(article.save)
            @articleID = article.id
          else
            puts "ERROR::getDataArticle:NoInsercion"
          end
        end
      end
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
        
        journal = Journal.new(:issn => issn, :title => @titleJournal, :country_id => @countryID, :publisher_id => 1 )
        
        if(!Journal.exists?(:issn => issn))
          if(journal.save)
            @journalID = journal.id
          else
            puts"ERROR::getDataJournal:NoInsercion"
          end
        end
        
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

      journal_issue = JournalIssue.new(:journal_id => @journalID, :number => number, :volume => volume, :year => year )
      
      if( @journalID == nil )
        puts"ERROR::getDataJournalIssue:JournalID_NIL"
      else
        if(!JournalIssue.exists?(:journal_id => @journalID, :number => number, :volume => volume, :year => year))
          if(journal_issue.save)
            @journalIssueID = journal_issue.id
          else
            puts"ERROR::getDataJournalIssue:NoInsercion"
          end
        end
      end
      
    }
  end

  def getDataCollection( record )
    record.each {|element|
      key = element[0]
      value = element[1]
      if key == "222"
        title = value.gsub("\$\$b", "")
        @titleJournal = title
      end
    }
  end
  
end

data1 = DataExtractor_new.new("../../../dataScieloIndex/dataDAT/clase30tmp.txt", "clase26abrExtract.txt")
data1.preProcessingFile( )
data1.collectionRecord( )
data1.preProcessingCollection( )
data1.prepareInsert( )

