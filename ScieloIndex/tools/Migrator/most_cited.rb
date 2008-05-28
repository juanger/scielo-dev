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
      file.puts "<h4>Autores más citados</h4>\n<table id='top_ten'>"
      @top_ten.each { |id, author, cites|
      		file.puts "<tr><td>#{link_to(author,"http://dev.scielo.unam.mx:300/cite_index/find_auxiliar/#{id}?cites=#{cites}")}</td><td>#{cites}</td>"
      }
      file.puts "</table>"
    }
    
  end
  
end

## Calculo del Top Ten estático

most_cited = MostCitedList.new()
most_cited.write_file()