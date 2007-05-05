##
## simple.rb
## Login : <virginia@scielo-dev1>
## Started on  Fri May  4 18:31:45 2007 Virginia Teodosio
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

