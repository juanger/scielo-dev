class Country < ActiveRecord::Base
validates_numericality_of :id
validates_presence_of :id,:name,:codek

end
