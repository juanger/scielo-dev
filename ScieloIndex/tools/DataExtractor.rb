##
## holaMundo.rb
## Login : <virginia@scielo-dev1>
## Started on  Mon Apr 30 17:07:48 2007 Virginia Teodosio
## $Id$
## 
## Copyright (C) 2007 Virginia Teodosio
## This program is free software; you can redistribute it and/or modify
## it under the terms of the GNU General Public License as published by
## the Free Software Foundation; either version 2 of the License, or
## (at your option) any later version.
## 
## This program is distributed in the hope that it will be useful,
## but WITHOUT ANY WARRANTY; without even the implied warranty of
## MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
## GNU General Public License for more details.
## 
## You should have received a copy of the GNU General Public License
## along with this program; if not, write to the Free Software
## Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
##

# An IO object being Enumerable, we can use 'each' directly on it
#! /usr/bin/ruby

class DataExtractor
  def initialize( nameOpen, nameExit )
    @name_open = nameOpen
    @name_exit = nameExit
    @data = Array::new()
    @recordCollection = Array::new()
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
    i = 0
    File.open( @name_exit ).each{ |line|
      
      array = line.split(" ")
      keyline = array [0];
      
      if ( key == keyline )
        cadena += line
      else
        key = keyline
        @data[i] = cadena
        cadena = ""
        i = i+1
      end
    }
    @data[i] = cadena
  end

  def preProcessingCollection( )
    i=0
    @data.each{ |element|
      array = element.split(" ")
      key = array[0].concat(" ")
      elementTmp = element.gsub(key,"")
      elementTmp = elementTmp.gsub("   L $$","   $$")
      @data[i]=elementTmp
      i = i+1
    }
    
    @data.each{ |record|
      record = @data[0]
      elements = record.split("\n")
      index = 0
      pairValues = Hash::new()
      elements.each{ |pair|
        pairs = pair.split("   ")
        pairValues.store(pairs[0],pairs[1])
      }
      @recordCollection[index] = pairValues
      index = index+1
    }
  end
  
  def prepareInsert( )
  end
end

data1 = DataExtractor.new("../../../dataScieloIndex/dataDAT/clase30tmp.txt", "clase26abrExtract.txt")
data1.preProcessingFile( )
data1.collectionRecord( )
data1.preProcessingCollection( )
