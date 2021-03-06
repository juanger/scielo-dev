class Journal < Collection
  set_table_name "journals"
  validates_length_of :abbrev, :maximum => 200, :allow_nil => true
  validates_length_of :issn, :maximum => 9, :allow_nil => true
  validates_format_of :abbrev, :with => /^[^\t\r\n\f]*$/
  validates_format_of :issn, :with => /^([0-9]{4}-[0-9]{3}[0-9X])?$/
  validates_numericality_of :id, :allow_nil => true, :only_integer => true, :greater_than_or_equal_to => 1
  validates_uniqueness_of :issn, :allow_nil => true

  has_many :journal_issues

  #FIXME: Checar en MedLine su guideline para la creacion de abreviaciones para los titulos.
  def as_vancouver
    self.title
  end
end
