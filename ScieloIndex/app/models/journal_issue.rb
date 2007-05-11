class JournalIssue < ActiveRecord::Base
  validates_presence_of :year
  validates_numericality_of :year, :journal_id, :only_integer => true
  validates_inclusion_of :year, :in => (Date.today.year - 1000)..(Date.today.year + 1)

  belongs_to :journal
  has_many :articles

  validates_associated :journal, :articles

  #FIXME: Checar el schema para agregar atributos para contener el mes y dia.
  def as_vancouver
    info = self.year.to_s + ";"
    if self.volume != nil and self.number != nil
      info << "#{self.volume}(#{self.number})"
    elsif self.volume != nil
      info << self.volume
    elsif self.number != nil
      info << "(#{self.number})"
    end
    info
  end
end
