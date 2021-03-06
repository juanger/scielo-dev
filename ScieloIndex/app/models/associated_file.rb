class AssociatedFile < ActiveRecord::Base
  attr_accessor :pdfdata, :htmldata
  validates_presence_of :filename, :article_id
  validates_length_of :filename, :in => 3..200
  validates_inclusion_of :article_id, :in => 1..99999
  validates_numericality_of :article_id, :only_integer => true
  validates_inclusion_of :id, :in => 1..99999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_format_of :filename, :with => /^[-a-zA-Z0-9áéíóúÁÉÍÓÚñÑ]*$/
  validates_uniqueness_of :article_id

  belongs_to :article
end
