class JournalIssue < ActiveRecord::Base
  validates_presence_of :year
  validates_length_of :number, :volume, :number_supplement, :volume_supplement, :maximum => 100, :allow_nil => true
  validates_length_of :title, :maximum => 200, :allow_nil => true
  validates_format_of :number, :volume, :number_supplement, :volume_supplement, :with => /^[-a-zA-Z0-9,. ]*$/
  validates_inclusion_of :year, :in => (Date.today.year - 1000)..(Date.today.year + 1)
  validates_inclusion_of :journal_id, :in => 1..9999
  validates_numericality_of :year, :journal_id, :only_integer => true
  validates_inclusion_of :id, :in => 1..9999, :allow_nil => true
  validates_numericality_of :id, :allow_nil => true, :only_integer => true
  validates_uniqueness_of :journal_id, :scope => [:number, :volume, :year, :number_supplement]
  validates_uniqueness_of :journal_id, :scope => [:number, :volume, :year, :volume_supplement]

  belongs_to :journal
  has_many :articles

  #validates_associated :journal, :articles

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
