class AssociatedFile < ActiveRecord::Base
  validates_presence_of :name, :pdfdata, :htmldata
  validates_length_of :name, :in => 3..200
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_inclusion_of :id, :in => 1..9999, :allow_nil => true
  validates_format_of :name, :with => /^[-a-zA-Z0-9áéíóúÁÉÍÓÚñÑ]*$/
  validates_uniqueness_of :name

  has_one :article
end
