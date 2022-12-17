using System.IO;

namespace ContractsApi.Models
{
    public class RequisitesModel
    {
        public string? MainOrganizationName { get; set; }
        public string? MainAddress { get; set; }
        public string? ChildOrganizationName { get; set; }
        public string? ChildAddress { get; set; }
        public string? ChildPhone { get; set; }
        public string? ChildEmail { get; set; }
        public string? ChildFax { get; set; }
        public string? ChildINN { get; set; }
        public string? ChildUFK { get; set; }
        public string? ChildBIK { get; set; }
        public string? ChildKS { get; set; }
        public string? ChildEKS { get; set; }
        public string? ChildOKVED { get; set; }
        public string? ChildOKPO { get; set; }
        public string? ChildPurposePayment { get; set; }
    }
}

//�����������:
//����� �� ���������� ��������������� �����������
//�����: 656049, �.�������, ��.������,61
//���������� ��������(������) �����
//�����: 658225, �.��������, ��.������, 200�, �������: (385 - 57) 4 - 45 - 35, ���./ ����: (385 - 57) 4 - 20 - 04
//� - mail: director @rb.asu.ru
//���������� ���������:
//��� 2225004738, ��� 220902001
//��� �� ���������� ���� (���������� �������� (������) ����� �/� 20176�39730)
//��������� ������� ����� ������//��� �� ���������� ���� �. ������� ��� 010173001
//������������ ����  03214643000000011700 ������ ������������ ���� 40102810045370000009
//����� 85.22, ���� 43688231
//���������� �������: 00000000000000000130 � �� ��������(��� ��������) �� ��������(� � ���� ��������), ��� �� ����������.