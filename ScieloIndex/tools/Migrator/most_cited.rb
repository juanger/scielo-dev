#!/usr/bin/env ruby
RAILS_ENV = 'development'
$KCODE='u'
require File.dirname(__FILE__) + '/../../config/environment'

class MostCitedList
  include ActionView::Helpers::UrlHelper
  
  def initialize()
    @top_ten = Author.top_ten()    
  end
  
  def write_file()
    partial = "../../app/views/shared/_topTen.rhtml"
    open(partial, "w") { |file|
      file.puts "<h3>Autores más citados</h3>\n<table id='top_ten'>"
      @top_ten.each { |id, author, cites|
      		file.puts link_to()"<tr><td>#{author}</td><td>#{cites}</td>", {:action => 'find_auxiliar', :id => id, :cites => cites})
      }
      file.puts "</table>"
    }
  end
  
end

## Calculo del Top Ten estático

most_cited = MostCitedList.new()
most_cited.write_file()