class Language < ActiveRecord::Base
  validates_presence_of :name, :code
  validates_length_of :name, :in => 1..30
  validates_length_of :code, :in => 2..3
  validates_inclusion_of :id, :in => 1..999, :allow_nil => true
  validates_numericality_of :id, :only_integer => true, :allow_nil => true
  validates_format_of :name, :with => /^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,30}$/
  validates_format_of :code, :with => /^[a-z]{2,3}[ ]?$/
  validates_uniqueness_of :code

  has_many :articles
  has_many :alternate_titles
end
