require "rubygems"
gem "activerecord"
require 'active_record'
require 'yaml'

ActiveRecord::Base.establish_connection(
  :adapter  => "postgresql",
  :host     => "localhost",
  :database => "scielo_development",
  :username => "scielo",
  :password => "" 
)

class Country < ActiveRecord::Base
end

Country.find(:all).each {|country|
  puts "Id: #{country.id}, Nombre: #{country.name}, Code: #{country.code}"  
}

