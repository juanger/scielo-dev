class AlternateTitle < ActiveRecord::Base
  validates_presence_of :title, :language_id, :article_id
  validates_length_of :title, :in => 1..9999
  validates_inclusion_of :id, :in => 1..99999, :allow_nil => true
  validates_numericality_of :id, :only_integer => true, :allow_nil => true
  validates_inclusion_of :language_id,  :in => 1..999
  validates_inclusion_of :article_id, :in => 1..99999
  validates_numericality_of :language_id, :article_id, :only_integer => true
  validates_uniqueness_of :title, :scope => [:language_id, :article_id]

  belongs_to :article
  belongs_to :language
end
